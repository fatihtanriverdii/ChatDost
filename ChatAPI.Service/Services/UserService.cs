using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
		{
			return await _userRepository.GetAllUsersAsync(cancellationToken);
		}

		public async Task<User?> GetUserById(int id)
		{
			return await _userRepository.GetUserByIdAsync(id);
		}

		public async Task AddUser(User user)
		{
			await _userRepository.AddUserAsync(user);
		}

		public async Task UpdateUser(User user)
		{
			await _userRepository.UpdateUserAsync(user);
		}
	}
}
