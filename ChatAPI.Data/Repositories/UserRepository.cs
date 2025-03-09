using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using ChatAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddUserAsync(User user, CancellationToken cancellationToken)
		{
			try
			{
				_context.Users.Add(user);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An error while creating user: " + ex);
			}
		}

		public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
		{
			try
			{
				return await _context.Users
					.ToListAsync(cancellationToken);
			} 
			catch (OperationCanceledException) 
			{
				return new List<User>();
			}
		}

		public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An error while getting user: " + ex);
			}
		}

		public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
		{
			try
			{
				_context.Entry(user).State = EntityState.Modified;
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An error while updating user: " + ex.Message);
			}
		}

		public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
				return user;
			}
			catch(Exception ex)
			{
				throw new Exception("An error while getting user: " + ex.Message);
			}
		}

		public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception("An Error while getting user: " + ex.Message);
			}
		}

		public async Task DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _context.Users.FindAsync(id, cancellationToken);
				_context.Users.Remove(user);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An Error while delete user: " + ex.Message);
			}
		}

		public async Task DeleteUserByUsernameAsync(string username, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(u =>u.Username == username, cancellationToken);
				_context.Users.Remove(user);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An Error while delete user: " + ex.Message);
			}
		}

		public async Task<AdminDashboardResponseDto> CountAllUsersAsync(CancellationToken cancellationToken)
		{
			try
			{
				var users = await _context.Users.CountAsync(cancellationToken);
				return new AdminDashboardResponseDto
				{
					IsSuccess = true,
					TotalUsers = users
				};
			}
			catch (Exception ex)
			{
				return new AdminDashboardResponseDto
				{
					IsSuccess = false,
					ErrorMessage = ex.Message
				};
			}
		}

		public async Task<AdminDashboardResponseDto> CountIsActiveUsersAsync(CancellationToken cancellationToken)
		{
			try
			{
				var activeUsers = await _context.Users.CountAsync(u => u.IsActive, cancellationToken);
				return new AdminDashboardResponseDto
				{
					IsSuccess = true,
					ActiveUsers = activeUsers
				};
			}
			catch (Exception ex)
			{
				return new AdminDashboardResponseDto
				{
					IsSuccess = false,
					ErrorMessage = ex.Message
				};
			}
		}
	}
}
