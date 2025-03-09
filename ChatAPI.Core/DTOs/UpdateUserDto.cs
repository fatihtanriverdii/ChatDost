namespace ChatAPI.Core.DTOs
{
	public class UpdateUserDto
	{
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public int Age { get; set; }
		public string Job { get; set; } = string.Empty;
	}
}