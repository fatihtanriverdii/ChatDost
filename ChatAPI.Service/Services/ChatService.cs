using AutoMapper;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;

namespace ChatAPI.Service.Services
{
	public class ChatService : IChatService
	{
		private readonly IChatRepository _chatRepository;
		private readonly IMapper _mapper;
		private readonly IChatHubService _chatHubService;

		public ChatService(IChatRepository chatRepository, IMapper mapper, IChatHubService chatHubService)
		{
			_chatRepository = chatRepository;
			_mapper = mapper;
			_chatHubService = chatHubService;
		}

		public async Task<int> CreateChatRoomAsync(string roomName, string connectionId, int userId, CancellationToken cancellationToken)
		{
			var roomId = await _chatRepository.CreateChatRoomAsync(roomName, userId, cancellationToken);
			await _chatHubService.JoinRoomAsync(connectionId, roomId);
			return roomId;
		}
		public async Task<bool> JoinRoomAsync(int userId, string connectionId, string roomCode, CancellationToken cancellationToken)
		{
			var response = await _chatRepository.JoinRoomAsync(userId, roomCode, cancellationToken);
			if (response.IsSuccess)
			{
				await _chatHubService.JoinRoomAsync(connectionId, response.RoomId);
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task<List<ChatRoomDto>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken)
		{
			var rooms = await _chatRepository.GetChatRoomsAsync(userId, cancellationToken);
			return _mapper.Map<List<ChatRoomDto>>(rooms);
		}

		public async Task<List<MessageResponseDto>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken)
		{
			return await _chatRepository.GetMessagesAsync(chatRoomId, cancellationToken);
		}

		public async Task<MessageResponseDto> SendMessageAsync(int senderId, SendMessageDto messageDto, CancellationToken cancellationToken)
		{
			var message = new Message
			{
				ChatRoomId = messageDto.ChatRoomId,
				SenderId = senderId,
				Content = messageDto.Content,
				SentAt = DateTime.UtcNow
			};
			
			var messageResponseDto = await _chatRepository.AddMessageAsync(message, cancellationToken);
			await _chatHubService.SendMessageToRoomAsync(messageDto.ChatRoomId, message.Content);

			return messageResponseDto;
		}
	}
}
