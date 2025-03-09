using ChatAPI.Core.DTOs;

namespace ChatAPI.Core.Interfaces
{
	public interface IAdminDashboardService
	{
		Task<AdminDashboardResponseDto> GetAdminDashboardStatsAsync(CancellationToken cancellationToken);
	}
}
