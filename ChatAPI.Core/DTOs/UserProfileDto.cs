using ChatAPI.Core.Enums;

namespace ChatAPI.Core.DTOs
{
	public class UserProfileDto
	{
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public int Age { get; set; }
		public string Job { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}
}
