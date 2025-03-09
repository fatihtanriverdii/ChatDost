using ChatAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
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
		public string PasswordHash { get; set; } = string.Empty;
		[Required]
		public UserRole Role { get; set; } = UserRole.User;
		[Required]
		public int Age { get; set; }
		[Required]
		public string Job { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }
	}
}
