using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.DTOs
{
	public class JoinRoomResponseDto
	{
		public bool IsSuccess { get; set; }
		public string? ErrorMessage { get; set; }
		public int? RoomId { get; set; }
	}
}
