namespace ChatAPI.Core.DTOs
{
	public class JoinRoomResponseDto
	{
		public bool IsSuccess { get; set; }
		public string? ErrorMessage { get; set; }
		public int RoomId { get; set; }
	}
}
