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
		Task<List<User>> GetAllUsers(CancellationToken cancellationToken);
		Task<User?> GetUserById(int id);
		Task AddUser(User user);
		Task UpdateUser(User user);
	}
}
