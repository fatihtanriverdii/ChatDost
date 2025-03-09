using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.API.Controllers
{
	[Route("api/users")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
		{
			var users = await _userService.GetAllUsersAsync(cancellationToken);
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserByIdAsync(id, cancellationToken);
			if(user == null)
				return NotFound("User not found!");
			return Ok(user);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> AddUserAsync([FromBody] User user, CancellationToken cancellationToken)
		{
			var response = await _userService.AddUserAsync(user, cancellationToken);
			if (response.IsSuccess)
				return Ok(response);
			return BadRequest(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto updateUserDto, int id, CancellationToken cancellationToken)
		{
			var response = await _userService.UpdateUserAsync(updateUserDto, id, cancellationToken);
			if(response.IsSuccess)
				return Ok(response);
			return BadRequest(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			var response = await _userService.DeleteUserByIdAsync(id, cancellationToken);
			if(response.IsSuccess)
				return Ok(response);
			return BadRequest(response);
		}

		[HttpDelete("remove/{username}")]
		public async Task<IActionResult> DeleteUserByUsername(string username, CancellationToken cancellationToken)
		{
			var response = await _userService.DeleteUserByUsernameAsync(username, cancellationToken);
			if (response.IsSuccess)
				return Ok(response);
			return BadRequest(response);
		}
	}
}
