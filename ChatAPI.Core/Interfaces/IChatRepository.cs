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
		Task<int> CreateChatRoomAsync(string roomName, int userId,CancellationToken cancellationToken);
		Task DeleteChatRoomByIdAsync(int roomId,  CancellationToken cancellationToken);
		Task<ChatRoom?> GetChatRoomByIdAsync(int chatRoomId, CancellationToken cancellationToken);
		Task<MessageResponseDto> AddMessageAsync(Message message, string username, CancellationToken cancellationToken);
		Task<List<MessageResponseDto>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken);
		Task<List<ChatRoom>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken);
		Task<JoinRoomResponseDto> JoinRoomAsync(int userId, string roomCode, CancellationToken cancellationToken);
		Task<AdminDashboardResponseDto> CountAllMessagesAsync(CancellationToken cancellationToken);
		Task<AdminDashboardResponseDto> CountAllChatRoomsAsync(CancellationToken cancellationToken);
	}
}
