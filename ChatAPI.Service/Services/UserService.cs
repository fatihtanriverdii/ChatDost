using AutoMapper;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;

namespace ChatAPI.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<List<UserProfileDto>> GetAllUsersAsync(CancellationToken cancellationToken)
		{
			var users = await _userRepository.GetAllUsersAsync(cancellationToken);
			return _mapper.Map<List<UserProfileDto>>(users);
		}

		public async Task<UserProfileDto> GetUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
			return _mapper.Map<UserProfileDto>(user);
		}

		public async Task<UserResponseDto> AddUserAsync(User user, CancellationToken cancellationToken)
		{
			var username = user.Username;
			var existUser = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
			if (existUser != null)
			{
				return new UserResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Username is already using"
				};
			}
			else
			{
				await _userRepository.AddUserAsync(user, cancellationToken);
				return new UserResponseDto
				{
					IsSuccess = true,
					Profile = _mapper.Map<UserProfileDto>(user),
				};
			}
	
		}

		public async Task<UserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto, int id, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
			if(user == null)
			{
				return new UserResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "User not found!"
				};
			}
			else
			{
				_mapper.Map(updateUserDto, user);
				if(updateUserDto.Password != null && updateUserDto.Password != "")
					user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);

				await _userRepository.UpdateUserAsync(user, cancellationToken);

				return new UserResponseDto
				{
					IsSuccess = true,
					Profile = _mapper.Map<UserProfileDto>(user)
				};
			}
		}

		public async Task<UserResponseDto> DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
			if (user == null)
			{
				return new UserResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "User not found!"
				};
			}
			else
			{
				await _userRepository.DeleteUserByIdAsync(id, cancellationToken);
				return new UserResponseDto
				{
					IsSuccess = true
				};
			}
		}

		public async Task<UserResponseDto> DeleteUserByUsernameAsync(string username, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
			if (user == null)
			{
				return new UserResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "User not found!"
				};
			}
			else
			{
				await _userRepository.DeleteUserByUsernameAsync(username, cancellationToken);
				return new UserResponseDto
				{
					IsSuccess = true
				};
			}
		}
	}
}
