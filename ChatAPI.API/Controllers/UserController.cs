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
			await _userService.AddUserAsync(user, cancellationToken);
			return CreatedAtAction(nameof(GetUserByIdAsync), new {id = user.Id}, user);
		}

	}
}
