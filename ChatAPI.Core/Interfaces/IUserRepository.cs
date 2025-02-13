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
		Task<List<User>> GetAllUser();
		Task<User?> GetUserById(int id);
		Task AddUser(User user);
		Task UpdateUser(User user);
	}
}
