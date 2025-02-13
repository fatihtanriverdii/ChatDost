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
	public class UserChatRoom
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ChatRoomId { get; set; }
		[ForeignKey(nameof(ChatRoomId))]
		public ChatRoom ChatRoom { get; set; }
		public User user { get; set; }
	}
}
