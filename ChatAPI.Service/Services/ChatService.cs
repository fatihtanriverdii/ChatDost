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

		public async Task<int> CreateChatRoom(string roomName, string connectionId, int userId)
		{
			var roomId = await _chatRepository.CreateChatRoom(roomName, userId);
			await _chatHubService.JoinRoom(connectionId, Convert.ToString(roomId));
			return roomId;
		}
		public async Task<bool> JoinRoom(int userId, string connectionId, string roomCode)
		{
			var roomId = await _chatRepository.JoinRoomAsync(userId, roomCode);
			if (roomId == 0)
			{
				return false;
			}
			else
			{
				await _chatHubService.JoinRoom(connectionId, Convert.ToString(roomId));
				return true;
			}
		}

		public async Task<List<ChatRoomDto>> GetChatRooms(int userId)
		{
			var rooms = await _chatRepository.GetChatRooms(userId);
			return _mapper.Map<List<ChatRoomDto>>(rooms);
		}

		public List<Message> GetMessages(int chatRoomId)
		{
			return _chatRepository.GetMessages(chatRoomId);
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
