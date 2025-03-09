using ChatAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.API.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api/admin/dashboard")]
	public class AdminDashboardController : ControllerBase
	{
		private readonly IAdminDashboardService _adminDashboardService;

		public AdminDashboardController(IAdminDashboardService adminDashboardService) {
			_adminDashboardService = adminDashboardService;
		}

		[HttpGet("stats")]
		public async Task<IActionResult> GetAdminDashboardStatsAsync(CancellationToken cancellationToken)
		{
			var response = await _adminDashboardService.GetAdminDashboardStatsAsync(cancellationToken);
			if(response.IsSuccess)
				return Ok(response);
			return BadRequest(response);
		}
	}
}
