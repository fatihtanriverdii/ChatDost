using ChatAPI.Core.DTOs;
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
	public class ChatRepository : IChatRepository
	{
		private readonly AppDbContext _context;

		public ChatRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<int> CreateChatRoomAsync(string roomName, int userId)
		{
			var chatRoom = new ChatRoom
			{
				Name = roomName,
				Code = Guid.NewGuid().ToString().Substring(0, 6)
			};

			var userChatRoom = new UserChatRoom
			{
				UserId = userId,
				ChatRoomId = chatRoom.Id,
				ChatRoom = chatRoom
			};
			chatRoom.Users.Add(userChatRoom);
			_context.ChatRooms.Add(chatRoom);
			_context.UserChatRooms.Add(userChatRoom);
			await _context.SaveChangesAsync();
			return chatRoom.Id;
		}

		public async Task<JoinRoomResponseDto> JoinRoomAsync(int userId, string roomCode)
		{
			var chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(c => c.Code == roomCode);
			if (chatRoom == null)
				return new JoinRoomResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "Chatroom is not exist or invalid room code"
				};

			var userInRoom = await _context.UserChatRooms
				.AnyAsync(ur => ur.UserId == userId && ur.ChatRoomId == chatRoom.Id);

			if (!userInRoom)
			{
				var userChatRoom = new UserChatRoom { UserId = userId, ChatRoomId = chatRoom.Id };
				_context.UserChatRooms.Add(userChatRoom);
				await _context.SaveChangesAsync();
				return new JoinRoomResponseDto
				{
					IsSuccess = true,
					RoomId = chatRoom.Id,
				};
			}
			else
			{
				return new JoinRoomResponseDto
				{
					IsSuccess = false,
					ErrorMessage = "The user already in the room"
				};
			}
		}

		public async Task<ChatRoom?> GetChatRoomByIdAsync(int chatRoomId)
		{
			return await _context.ChatRooms.FirstOrDefaultAsync(c => c.Id == chatRoomId);
		}

		public async Task<List<ChatRoom>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken)
		{
			return await _context.ChatRooms
			.Where(r => r.Users.Any(u => u.UserId == userId))
			.Include(r => r.Users)
			.ToListAsync(cancellationToken);
		}

		public async Task<List<Message>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken)
		{
			return await _context.Messages
				.Where(m => m.ChatRoomId == chatRoomId)
				.OrderBy(m => m.SentAt)
				.ToListAsync(cancellationToken);
		}

		public async Task<Message> AddMessageAsync(Message message)
		{
			_context.Messages.Add(message);
			await _context.SaveChangesAsync();
			return message;
		}
	}
}
