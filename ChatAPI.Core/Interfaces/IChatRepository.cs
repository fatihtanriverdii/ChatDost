using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IChatRepository
	{
		Task<int> CreateChatRoom(string roomName, int userId);
		ChatRoom? GetChatRoomById(int chatRoomId);
		Task<Message> AddMessageAsync(Message message);
		List<Message> GetMessages(int chatRoomId);
		Task<List<ChatRoom>> GetChatRooms(int userId);
		Task<int> JoinRoomAsync(int userId, string roomCode);
	}
}
