using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command.Attributes
{
	public abstract class DecorateAttribute : Attribute
	{
		public abstract AttributedCommand<BotT, MessageT> Decorate<BotT, MessageT>(Command<BotT, MessageT> command)
			where BotT : class, IBot
			where MessageT : class, IMessage;
	}
}
