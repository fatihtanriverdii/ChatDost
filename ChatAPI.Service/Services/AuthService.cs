using AutoMapper;
using Azure;
using BCrypt.Net;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Service.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;	

		public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<string> Login(LoginDto loginDto)
		{
			var users = await _userRepository.GetAllUser();
			var user = users.FirstOrDefault(u => u.Username == loginDto.Username);
			if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
				return null;

			user.RefreshToken = GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

			await _userRepository.UpdateUser(user);

			_httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", user.RefreshToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				Expires = DateTime.UtcNow.AddDays(7),
				SameSite = SameSiteMode.None
			});

			return GenerateJwtToken(user);
		}

		public async Task<string> Register(RegisterDto registerDto)
		{
			var users = await _userRepository.GetAllUser();
			if(users.Any(u => u.Username == registerDto.Username))
				return null;

			var newUser = _mapper.Map<User>(registerDto);

			newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

			await _userRepository.AddUser(newUser);

			return GenerateJwtToken(newUser);
		}

		private string GenerateJwtToken(User user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim("UserId", user.Id.ToString())
			};

			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new Byte[32];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public async Task<string> RefreshToAccessToken(string refreshToken)
		{
			var users = await _userRepository.GetAllUser();
			var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

			if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
				return null;

			var newAccessToken = GenerateJwtToken(user);

			return newAccessToken;
		}
	}
}
