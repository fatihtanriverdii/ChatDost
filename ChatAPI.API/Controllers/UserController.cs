using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using ChatAPI.Service.Services;
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
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
		{
			var users = await _userService.GetAllUsers(cancellationToken);
			return Ok(users);
		}

		[HttpGet("{id}")]
		public IActionResult GetUserById(int id)
		{
			var user = _userService.GetUserById(id);
			if(user == null)
				return NotFound("User not found!");
			return Ok(user);
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult AddUser([FromBody] User user)
		{
			var createdUser = _userService.AddUser(user);
			return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
		}

	}
}
