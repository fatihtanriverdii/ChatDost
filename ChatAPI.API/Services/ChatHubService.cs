using ChatAPI.API.Hubs;
using ChatAPI.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.API.Services
{
	public class ChatHubService : IChatHubService
	{
		private readonly IHubContext<ChatHub> _hubContext;
		private const string ReceiveMessageMethod = "ReceiveMessage";
		public ChatHubService(IHubContext<ChatHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task JoinRoomAsync(string connectionId, int roomId)
		{
			try
			{
				await _hubContext.Groups.AddToGroupAsync(connectionId, roomId.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while adding connection {connectionId} to room {roomId}: {ex.Message}");
			}

		}

		public async Task SendMessageToRoomAsync(int roomId, string message)
		{
			await _hubContext.Clients.Group(roomId.ToString()).SendAsync(ReceiveMessageMethod, message);
		}
	}
}
