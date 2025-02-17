using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IChatHubService
	{
		Task SendMessageToRoomAsync(int roomId, string message);
		Task JoinRoomAsync(string connectionId, int roomId);
	}
}
