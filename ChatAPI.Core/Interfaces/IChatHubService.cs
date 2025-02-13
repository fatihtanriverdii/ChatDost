using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Interfaces
{
	public interface IChatHubService
	{
		Task SendMessageToRoom(int roomId, string message);
		Task JoinRoom(string connectionId, string roomId);
	}
}
