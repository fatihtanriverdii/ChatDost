using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using ChatAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddUser(User user)
		{
			try
			{
				_context.Users.Add(user);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("An error while creating user: ", ex);
			}
		}

		public async Task<List<User>> GetAllUser()
		{
			try
			{
				return await _context.Users.ToListAsync();
			} 
			catch (Exception ex) 
			{
				throw new Exception("An error while getting users: ", ex);
			}
		}

		public async Task<User?> GetUserById(int id)
		{
			try
			{
				return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception("An error while getting user: ", ex);
			}
		}

		public async Task UpdateUser(User user)
		{
			try
			{
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("An error while updating user: ", ex);
			}
		}
	}
}
