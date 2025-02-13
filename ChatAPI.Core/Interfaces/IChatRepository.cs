using ChatAPI.Core.DTOs;
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
		Task<int> CreateChatRoomAsync(string roomName, int userId);
		Task<ChatRoom?> GetChatRoomByIdAsync(int chatRoomId);
		Task<Message> AddMessageAsync(Message message);
		Task<List<Message>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken);
		Task<List<ChatRoom>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken);
		Task<JoinRoomResponseDto> JoinRoomAsync(int userId, string roomCode);
	}
}
