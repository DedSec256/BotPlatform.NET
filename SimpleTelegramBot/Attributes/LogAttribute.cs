using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System.Attributes;
using BotPlatfrom.Kernel.System.Commands;
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

	class LogAttributedCommand<TBot, TMessage> : AttributedCommand<TBot, TMessage> 
	{
		public LogAttributedCommand(Command<TBot, TMessage> baseCommand) : base(baseCommand)
		{ }
		public override void BeforeExecute(TBot message, TMessage bot, object arg = null)
		{
			Console.WriteLine("Logging...");
		}
	}
}
