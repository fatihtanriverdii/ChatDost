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
		public async Task<IActionResult> CreateChatRoomAsync([FromBody] CreateRoomDto createRoomDto, CancellationToken cancellationToken)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (userId == null)
				return Unauthorized("User not authenticated!");

			var connectionId = ChatHub.GetConnectionId(int.Parse(userId));
			var chatRoomId = await _chatService.CreateChatRoomAsync(createRoomDto.RoomName, connectionId, int.Parse(userId), cancellationToken);
			return Ok(chatRoomId);
		}

		[HttpDelete("room/{roomId}")]
		public async Task<IActionResult> DeleteChatRoomByIdAsync(int roomId, CancellationToken cancellationToken)
		{
			var response = await _chatService.DeleteChatRoomByIdAsync(roomId, cancellationToken);
			if(response)
				return Ok("Delete is successful");
			return BadRequest();
		}

		[HttpPost("join-room")]
		public async Task<IActionResult> JoinRoomAsync([FromBody] JoinRoomDto joinRoomDto, CancellationToken cancellationToken)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if(userId == null)
				return Unauthorized("User not authenticated!");

			var connectionId = ChatHub.GetConnectionId(int.Parse(userId));
			var result = await _chatService.JoinRoomAsync(int.Parse(userId), connectionId, joinRoomDto.RoomCode, cancellationToken);
			return result ? Ok("Joined successfully") : BadRequest("Invalid room code!");
		}


		[HttpPost("send-message")]
		public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageDto messageDto, CancellationToken cancellationToken)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			var username = User.FindFirst(ClaimTypes.Name)?.Value;

			if (userId == null)
				return Unauthorized("User not authenticated!");

			var sendM = await _chatService.SendMessageAsync(int.Parse(userId), username, messageDto, cancellationToken);
			if (sendM.IsSuccess)
			{
				return Ok(sendM);
			}
			else
			{
				return BadRequest("Error: " + sendM.ErrorMessage);
			}
		}

		[HttpGet("chat-rooms")]
		public async Task<IActionResult> GetChatRoomsAsync(CancellationToken cancellationToken)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

			if(string.IsNullOrEmpty(userId))
				return Unauthorized("User not found!");

			var chatRooms = await _chatService.GetChatRoomsAsync(int.Parse(userId), cancellationToken);
			return Ok(chatRooms);
		}

		[HttpGet("messages/{chatRoomId}")]
		public async Task<IActionResult> GetMessagesAsync(int chatRoomId, CancellationToken cancellationToken) 
		{
			var messages = await _chatService.GetMessagesAsync(chatRoomId, cancellationToken);
			return Ok(messages);
		}
	}
}
