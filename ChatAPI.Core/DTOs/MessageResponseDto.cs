namespace ChatAPI.Core.DTOs
{
	public class MessageResponseDto
	{
		public bool IsSuccess { get; set; }
		public int? ChatRoomId { get; set; }
		public int? SenderId { get; set; }
		public string? Content { get; set; }
		public DateTime? SentAt { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
