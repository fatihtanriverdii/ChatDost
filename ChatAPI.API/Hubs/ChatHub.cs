using ChatAPI.Core.Interfaces;
using ChatAPI.Core.Models;
using ChatAPI.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.API.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private static readonly Dictionary<int, string> _userConnections = new();
		private readonly IChatService _chatService;

		public ChatHub(IChatService chatService)
		{
			_chatService = chatService;
		}
		public override async Task OnConnectedAsync()
		{
			try
			{
				var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
				var connectionId = Context.ConnectionId;

				if (!string.IsNullOrEmpty(userId))
				{
					int parsedUserId = int.Parse(userId);
					_userConnections[parsedUserId] = connectionId;

					var userRooms = await _chatService.GetChatRooms(parsedUserId);
					foreach (var room in userRooms)
					{
						await Groups.AddToGroupAsync(Context.ConnectionId, Convert.ToString(room.Id));
					}
				}
				else
				{
					Console.WriteLine("UserId is null");
				}

				await base.OnConnectedAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Error", ex);
			}
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (userId != null)
			{
				int parsedUserId = int.Parse(userId);
				_userConnections.Remove(parsedUserId);
			}

			await base.OnDisconnectedAsync(exception);
		}

		public static string? GetConnectionId(int userId)
		{
			return _userConnections[userId];
		}
	}
}