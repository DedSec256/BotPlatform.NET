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
		public abstract AttributedCommand<BotT> Decorate<BotT>(Command<BotT> command) where BotT : class, ISingleBot;
	}
}
