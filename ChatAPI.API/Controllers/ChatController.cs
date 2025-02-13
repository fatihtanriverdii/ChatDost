using ChatAPI.API.Hubs;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAPI.API.Controllers
{
	[Route("api/chat")]
	[ApiController]
	[Authorize]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;

		public ChatController(IChatService chatService)
		{
			_chatService = chatService;
		}

		[HttpPost("create-room")]
		public async Task<IActionResult> CreateChatRoomAsync([FromBody] CreateRoomDto createRoomDto)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (userId == null)
				return Unauthorized("User not authenticated!");

			var connectionId = ChatHub.GetConnectionId(int.Parse(userId));
			var chatRoomId = await _chatService.CreateChatRoomAsync(createRoomDto.roomName, connectionId, int.Parse(userId));
			return Ok(chatRoomId);
		}

		[HttpPost("join-room")]
		public async Task<IActionResult> JoinRoom([FromBody] JoinRoomDto joinRoomDto)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if(userId == null)
				return Unauthorized("User not authenticated!");

			var connectionId = ChatHub.GetConnectionId(int.Parse(userId));
			var result = await _chatService.JoinRoomAsync(int.Parse(userId), connectionId, joinRoomDto.roomCode);
			return result ? Ok("Joined successfully") : BadRequest("Invalid room code!");
		}


		[HttpPost("send-message")]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (userId == null)
				return Unauthorized("User not authenticated!");

			var message = await _chatService.SendMessageAsync(int.Parse(userId), messageDto);
			return Ok(message);
		}

		[HttpGet("chat-rooms")]
		public async Task<IActionResult> GetChatRooms(CancellationToken cancellationToken)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

			if(string.IsNullOrEmpty(userId))
				return Unauthorized("User not found!");

			var chatRooms = await _chatService.GetChatRoomsAsync(int.Parse(userId), cancellationToken);
			return Ok(chatRooms);
		}

		[HttpGet("messages/{chatRoomId}")]
		public IActionResult GetMessages(int chatRoomId, CancellationToken cancellationToken) 
		{
			var messages = _chatService.GetMessagesAsync(chatRoomId, cancellationToken);
			return Ok(messages);
		}
	}
}
