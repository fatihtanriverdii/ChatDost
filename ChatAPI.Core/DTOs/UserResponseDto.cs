using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.DTOs
{
	public class UserResponseDto
	{
		public bool IsSuccess { get; set; }
		public string? ErrorMessage { get; set; }
		public UserProfileDto? Profile { get; set; }
	}
}
