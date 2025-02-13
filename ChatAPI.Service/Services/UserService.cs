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

		public async Task<List<User>> GetAllUsers()
		{
			return await _userRepository.GetAllUser();
		}

		public async Task<User?> GetUserById(int id)
		{
			return await _userRepository.GetUserById(id);
		}

		public async Task AddUser(User user)
		{
			await _userRepository.AddUser(user);
		}

		public async Task UpdateUser(User user)
		{
			await _userRepository.UpdateUser(user);
		}
	}
}
