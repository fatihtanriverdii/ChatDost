namespace ChatAPI.Core.DTOs
{
	public class ChatRoomDto
	{
		public int Id {  get; set; }
		public string RoomName { get; set; } = string.Empty;
		public string RoomCode { get; set; } = string.Empty;
		public int UserCount { get; set; }
		public string? LastMessage { get; set; }
	}
}
