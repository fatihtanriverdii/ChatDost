using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.DTOs
{
	public class AuthResponseDto
	{
		public bool IsSuccess {  get; set; }
		public string? Token { get; set; }
		public DateTime? Expiration { get; set; }
		public string? ErrorMessage { get; set; }
	}
}