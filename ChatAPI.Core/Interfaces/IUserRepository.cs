using ChatAPI.Core.DTOs;
using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
		Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
		Task AddUserAsync(User user, CancellationToken cancellationToken);
		Task UpdateUserAsync(User user, CancellationToken cancellationToken);
		Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
		Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
		Task DeleteUserByIdAsync(int id, CancellationToken cancellationToken);
		Task DeleteUserByUsernameAsync(string username, CancellationToken cancellationToken);
		Task<AdminDashboardResponseDto> CountAllUsersAsync(CancellationToken cancellationToken);
		Task<AdminDashboardResponseDto> CountIsActiveUsersAsync(CancellationToken cancellationToken);
	}
}
