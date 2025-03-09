using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;

namespace ChatAPI.Service.Services
{
	public class AdminDashboardService : IAdminDashboardService
	{
		private readonly IUserRepository _userRepository;
		private readonly IChatRepository _chatRepository;

		public AdminDashboardService(IChatRepository chatRepository, IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_chatRepository = chatRepository;
		}
		public async Task<AdminDashboardResponseDto> GetAdminDashboardStatsAsync(CancellationToken cancellationToken)
		{
			var totalUsersResponse = await _userRepository.CountAllUsersAsync(cancellationToken);
			var activeUsersResponse = await _userRepository.CountIsActiveUsersAsync(cancellationToken);
			var totalMessagesResponse = await _chatRepository.CountAllMessagesAsync(cancellationToken);
			var	totalGroupsResponse = await _chatRepository.CountAllChatRoomsAsync(cancellationToken);

			if (totalUsersResponse.IsSuccess && activeUsersResponse.IsSuccess && totalMessagesResponse.IsSuccess && totalGroupsResponse.IsSuccess)
			{
				return new AdminDashboardResponseDto
				{
					IsSuccess = true,
					TotalUsers = totalUsersResponse.TotalUsers,
					ActiveUsers = activeUsersResponse.ActiveUsers,
					TotalMessages = totalMessagesResponse.TotalMessages,
					TotalGroups = totalGroupsResponse.TotalGroups,
				};
			}
			return new AdminDashboardResponseDto
			{
				IsSuccess = false,
				ErrorMessage = $"{totalUsersResponse.ErrorMessage}, {activeUsersResponse.ErrorMessage}, {totalMessagesResponse.ErrorMessage}, {totalGroupsResponse.ErrorMessage}"
			};
		}
	}
}
