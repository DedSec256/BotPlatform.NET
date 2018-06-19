using BotPlatfrom.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using SimpleTelegramBotExample.Attributes;
using SimpleTelegramBotExample.Implementations;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleTelegramBotExample.Modules
{
	class SimpleCommands : CommandsModule<TelegramBotClient, Message>
	{
		public SimpleCommands() { }
		public override void Initialize()
		{
			CommandCenter.TryAdd("/help", HelpCallback);
		}

		[Log]
		private void HelpCallback(Message message, TelegramBotClient bot, object arg = null)
		{
			bot.SendTextMessageAsync(message.From.Id, $"Hello, user{message.From.Id}: ...");
		}

	}
}
