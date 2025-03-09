using AutoMapper;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ChatAPI.Service.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_configuration = configuration;		
		}

		public async Task<AuthResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username, cancellationToken);
			if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
			{
				return new AuthResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Username or Password invalid"
				};
			}
			else
			{
				user.RefreshToken = GenerateRefreshToken();
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

				await _userRepository.UpdateUserAsync(user, cancellationToken);


				var token = GenerateJwtToken(user);

				return new AuthResponseDto
				{
					IsSuccess = true,
					Token = token,
					RefreshToken = user.RefreshToken,
					Expiration = user.RefreshTokenExpiryTime
				};
			}
		}

		public async Task<AuthResponseDto> LoginAdminAsync(LoginDto loginDto, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username, cancellationToken);
			if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
			{
				return new AuthResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Username or Password invalid"
				};
			}
			else if (user.Role == 0)
			{
				user.RefreshToken = GenerateRefreshToken();
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

				var token = GenerateJwtToken(user);

				return new AuthResponseDto
				{
					IsSuccess = true,
					Token = token,
					RefreshToken = user.RefreshToken,
					Expiration = user.RefreshTokenExpiryTime
				};
			}
			else
			{
				return new AuthResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "invalid authorization"
				};
			}
		}

		public async Task<AuthResponseDto> Register(RegisterDto registerDto, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByUsernameAsync(registerDto.Username, cancellationToken);
			if (user != null)
				return new AuthResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Username already exist"
				};

			var newUser = _mapper.Map<User>(registerDto);
			newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
			await _userRepository.AddUserAsync(newUser, cancellationToken);
			var token = GenerateJwtToken(newUser);

			return new AuthResponseDto
			{
				IsSuccess = true,
				Token = token,
				Expiration = DateTime.UtcNow.AddDays(7)
			};
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

		public async Task<AuthResponseDto> RefreshToAccessToken(string refreshToken, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken, cancellationToken);

			if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
				return new AuthResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Token expiration is over or user not found"
				};

			var newAccessToken = GenerateJwtToken(user);

			return new AuthResponseDto
			{
				IsSuccess = true,
				Token = newAccessToken,
				Expiration = user.RefreshTokenExpiryTime
			};
		}
	}
}
