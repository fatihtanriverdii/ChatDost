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
		Task<int> CreateChatRoomAsync(string roomName, string connectionId, int userId, CancellationToken cancellationToken);
		Task<bool> DeleteChatRoomByIdAsync(int roomId, CancellationToken cancellationToken);
		Task<bool> JoinRoomAsync(int userId, string connectionId, string roomCode, CancellationToken cancellationToken);
		Task<MessageResponseDto> SendMessageAsync(int senderId, string username, SendMessageDto messageDto, CancellationToken cancellationToken);
		Task<List<MessageResponseDto>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken);
		Task<List<ChatRoomDto>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken);
	}
}
