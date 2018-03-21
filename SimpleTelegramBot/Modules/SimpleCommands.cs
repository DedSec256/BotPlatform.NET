using BotPlatfrom.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using SimpleTelegramBotExample.Attributes;
using SimpleTelegramBotExample.Implementations;

namespace SimpleTelegramBotExample.Modules
{
	class SimpleCommands : ICommandsModule
	{
		public SimpleCommands() { }
		public void Initialize()
		{
			CommandCenter.Instance.TryAdd<SimpleTelegramBot>("help", HelpCallback);
		}

		[Log]
		private void HelpCallback(Message message, SimpleTelegramBot bot, object obj = null)
		{
			bot.Instanse.SendTextMessageAsync(message.UserId, $"Здравствуйте, user{message.UserId}: ...");
		}
	}
}
