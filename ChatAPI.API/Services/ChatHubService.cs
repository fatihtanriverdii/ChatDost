using ChatAPI.API.Hubs;
using ChatAPI.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.API.Services
{
	public class ChatHubService : IChatHubService
	{
		private readonly IHubContext<ChatHub> _hubContext;
		public ChatHubService(IHubContext<ChatHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task JoinRoomAsync(string connectionId, int roomId)
		{
			await _hubContext.Groups.AddToGroupAsync(connectionId, roomId.ToString());
		}

		public async Task SendMessageToRoomAsync(int roomId, string message)
		{
			await _hubContext.Clients.Group(roomId.ToString()).SendAsync("Receive Message", message);
		}
	}
}
