﻿using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Core.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Surname { get; set; } = string.Empty;
		[Required]
		public string Username { get; set; } = string.Empty;
		[Required]
		public string Gender { get; set; } = string.Empty;
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public int Age { get; set; }
		[Required]
		public string Job {	get; set; } = string.Empty;
	}
}
