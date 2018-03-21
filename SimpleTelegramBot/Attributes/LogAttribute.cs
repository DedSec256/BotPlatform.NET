using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using BotPlatfrom.Kernel.Command.Attributes;
using BotPlatfrom.Kernel.Interfaces;

namespace SimpleTelegramBotExample.Attributes
{
	class LogAttribute : DecorateAttribute
	{
		public override AttributedCommand<BotT> Decorate<BotT>(Command<BotT> command)
		{
			return new LogAttributedCommand<BotT>(command);
		}
	}

	internal class LogAttributedCommand<BotT> : AttributedCommand<BotT> where BotT: class, ISingleBot
	{
		public LogAttributedCommand(Command<BotT> baseCommand) : base(baseCommand)
		{
		}

		public override void AfterExecute(Message message, BotT bot, object arg = null)
		{
			Console.WriteLine("Logging...");
			BaseCommand.AfterExecute(message, bot, arg);
		}
	}
}
