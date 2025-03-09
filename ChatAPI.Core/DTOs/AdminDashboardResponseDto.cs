using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.DTOs
{
	public class AdminDashboardResponseDto
	{
		public bool IsSuccess { get; set; }
		public int TotalUsers { get; set; }
		public int ActiveUsers { get; set; }
		public int TotalMessages { get; set; }
		public int TotalGroups { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
