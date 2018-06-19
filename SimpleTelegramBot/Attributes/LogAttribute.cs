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
		public override AttributedCommand<TBot, TMessage> Decorate<TBot, TMessage>(Command<TBot, TMessage> command)
		{
			return new LogAttributedCommand<TBot, TMessage>(command);
		}
	}

	internal class LogAttributedCommand<TBot, TMessage> : AttributedCommand<TBot, TMessage> 
	{
		public LogAttributedCommand(Command<TBot, TMessage> baseCommand) : base(baseCommand)
		{ }
		public override void BeforeExecute(TMessage message, TBot bot, object arg = null)
		{
			Console.WriteLine("Logging...");
		}
	}
}
