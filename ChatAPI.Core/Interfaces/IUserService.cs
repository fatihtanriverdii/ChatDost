using ChatAPI.Core.DTOs;
using ChatAPI.Core.Models;

namespace ChatAPI.Core.Interfaces
{
	public interface IUserService
	{
		Task<List<UserProfileDto>> GetAllUsersAsync(CancellationToken cancellationToken);
		Task<UserProfileDto> GetUserByIdAsync(int id, CancellationToken cancellationToken);
		Task<UserResponseDto> AddUserAsync(User user, CancellationToken cancellationToken);
		Task<UserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto, int id, CancellationToken cancellationToken);
		Task<UserResponseDto> DeleteUserByIdAsync(int id, CancellationToken cancellationToken);
		Task<UserResponseDto> DeleteUserByUsernameAsync(string username, CancellationToken cancellationToken);
	}
}
