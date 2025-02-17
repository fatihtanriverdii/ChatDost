using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Core.DTOs
{
	public class SendMessageDto
	{
		[Required]
		public int ChatRoomId { get; set; }
		[Required]
		public string? Content { get; set; }
	}
}
