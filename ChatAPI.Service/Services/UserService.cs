using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;

namespace ChatAPI.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
		{
			return await _userRepository.GetAllUsersAsync(cancellationToken);
		}

		public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _userRepository.GetUserByIdAsync(id, cancellationToken);
		}

		public async Task AddUserAsync(User user, CancellationToken cancellationToken)
		{
			await _userRepository.AddUserAsync(user, cancellationToken);
		}

		public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
		{
			await _userRepository.UpdateUserAsync(user, cancellationToken);
		}
	}
}
