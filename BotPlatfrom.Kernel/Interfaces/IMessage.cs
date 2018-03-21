using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface IMessage
	{
		string Text { get; set; }
		long UserId { get; set; }
	}

	public interface IUniversalMessage : IMessage
	{
		string Caption { get; set; }
		DateTime Date { get; set; }
		long MessageId { get; set; }
		/* ... */
	}
}
