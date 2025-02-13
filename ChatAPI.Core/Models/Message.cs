using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Core.Models
{
	public class Message
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public int ChatRoomId { get; set; }
		[Required]
		public int SenderId { get; set; }
		[ForeignKey("SenderId")]
		public User Sender { get; set; }
		[Required]
		public string Content { get; set; } = string.Empty;
		[Required]
		public DateTime SentAt { get; set; }
		[ForeignKey("ChatRoomId")]
		public ChatRoom ChatRoom { get; set; }
	}
}
