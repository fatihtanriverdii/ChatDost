using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatAPI.Core.Models
{
	public class ChatRoom
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Code { get; set; } = string.Empty;
		public ICollection<UserChatRoom> Users { get; set; } = new List<UserChatRoom>();
		public ICollection<Message> Messages { get; set; } = new List<Message>();
	}
}
