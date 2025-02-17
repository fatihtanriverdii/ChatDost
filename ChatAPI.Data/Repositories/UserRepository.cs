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
				_context.Users.Update(user);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new Exception("An error while updating user: " + ex);
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
				throw new Exception("An error while getting user: " + ex);
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
				throw new Exception("An Error while getting user: " + ex);
			}
		}
	}
}
