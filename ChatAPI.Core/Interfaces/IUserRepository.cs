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
		Task<User?> GetUserByIdAsync(int id);
		Task AddUserAsync(User user);
		Task UpdateUserAsync(User user);
	}
}
