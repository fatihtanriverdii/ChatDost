using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterDto registerDto)
		{
			var token = _authService.Register(registerDto);
			if (token == null)
				return BadRequest("User already exist!");

			return Ok(new { Token = token });
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginDto loginDto)
		{
			var token = _authService.Login(loginDto);
			if (token == null)
				return Unauthorized("Invalid credentials");

			return Ok(new { Token = token });
		}

		[HttpPost("refresh")]
		public IActionResult RefreshToken()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			if(string.IsNullOrEmpty(refreshToken))
				return Unauthorized("Refresh token not found!");

			var newAccessToken = _authService.RefreshToAccessToken(refreshToken);
			if (newAccessToken == null)
				return Unauthorized("Invalid or expired refresh token!");

			return Ok(new { AccessToken = newAccessToken });
		}
	}
}
