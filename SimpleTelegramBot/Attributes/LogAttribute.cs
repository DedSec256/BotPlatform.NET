using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using BotPlatfrom.Kernel.Command.Attributes;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.Tools;

namespace SimpleTelegramBotExample.Attributes
{
	class LogAttribute : DecorateAttribute
	{
		public override AttributedCommand<BotT, MessageT> Decorate<BotT, MessageT>(Command<BotT, MessageT> command)
		{
			return new LogAttributedCommand<BotT, MessageT>(command);
		}
	}

	internal class LogAttributedCommand<BotT, MessageT> : AttributedCommand<BotT, MessageT> 
		where BotT: class, IBot
		where MessageT: class, IMessage
	{
		public LogAttributedCommand(Command<BotT, MessageT> baseCommand) : base(baseCommand)
		{
		}

		public override void BeforeExecute(MessageT message, BotT bot, object arg = null)
		{
			Console.WriteLine("Logging...");
		}
	}
}
