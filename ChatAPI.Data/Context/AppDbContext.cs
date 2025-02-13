using ChatAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Data.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<ChatRoom> ChatRooms { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<UserChatRoom> UserChatRooms { get; set; }
	}
}
