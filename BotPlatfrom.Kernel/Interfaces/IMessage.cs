using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public class Message
	{
		public string Text { get; set; }
		public string Caption { get; set; }
		public DateTime Date { get; set; }
		public long MessageId { get; set; }
		public long UserId { get; set; }
		/* ... */
	}
}
