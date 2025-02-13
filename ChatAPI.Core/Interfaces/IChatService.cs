using ChatAPI.Core.DTOs;
using ChatAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IChatService
	{
		Task<int> CreateChatRoom(string roomName, string connectionId, int userId);
		Task<bool> JoinRoom(int userId, string connectionId, string roomCode);
		Task<Message> SendMessageAsync(int senderId, SendMessageDto messageDto);
		List<Message> GetMessages(int chatRoomId);
		Task<List<ChatRoomDto>> GetChatRooms(int userId);
	}
}
