using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IUserService
	{
		Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
		Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
		Task AddUserAsync(User user, CancellationToken cancellationToken);
		Task UpdateUserAsync(User user, CancellationToken cancellationToken);
	}
}
