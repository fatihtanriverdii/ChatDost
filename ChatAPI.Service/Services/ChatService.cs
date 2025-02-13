using AutoMapper;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public async Task<int> CreateChatRoomAsync(string roomName, string connectionId, int userId)
		{
			var roomId = await _chatRepository.CreateChatRoomAsync(roomName, userId);
			await _chatHubService.JoinRoom(connectionId, Convert.ToString(roomId));
			return roomId;
		}
		public async Task<bool> JoinRoomAsync(int userId, string connectionId, string roomCode)
		{
			var response = await _chatRepository.JoinRoomAsync(userId, roomCode);
			if (response.IsSuccess)
			{
				await _chatHubService.JoinRoom(connectionId, Convert.ToString(response.RoomId));
				return true;
			}
			else
			{
				Console.WriteLine($"Error: {response.ErrorMessage}");
				return false;
			}
		}

		public async Task<List<ChatRoomDto>> GetChatRoomsAsync(int userId, CancellationToken cancellationToken)
		{
			var rooms = await _chatRepository.GetChatRoomsAsync(userId, cancellationToken);
			return _mapper.Map<List<ChatRoomDto>>(rooms);
		}

		public async Task<List<Message>> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken)
		{
			return await _chatRepository.GetMessagesAsync(chatRoomId, cancellationToken);
		}

		public async Task<Message> SendMessageAsync(int senderId, SendMessageDto messageDto)
		{
			var message = new Message
			{
				ChatRoomId = messageDto.ChatRoomId,
				SenderId = senderId,
				Content = messageDto.Content,
				SentAt = DateTime.UtcNow
			};
			
			message = await _chatRepository.AddMessageAsync(message);

			await _chatHubService.SendMessageToRoom(messageDto.ChatRoomId, message.Content);
			return message;
		}
	}
}
