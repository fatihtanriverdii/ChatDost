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
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.Register(registerDto, cancellationToken);
			if (authResponse.IsSuccess)
				return Ok(authResponse);
			return BadRequest(authResponse);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
		{
			var authResponse = await _authService.Login(loginDto, cancellationToken);
			if (authResponse.IsSuccess)
				return Ok(authResponse);
			return Unauthorized(authResponse);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
		{
			var refreshToken = Request.Cookies["refreshToken"];
			if(string.IsNullOrEmpty(refreshToken))
				return Unauthorized("Refresh token not found!");

			var authResponse = await _authService.RefreshToAccessToken(refreshToken, cancellationToken);
			if (authResponse.IsSuccess)
				return Ok(authResponse);

			return Unauthorized(authResponse);
		}
	}
}
