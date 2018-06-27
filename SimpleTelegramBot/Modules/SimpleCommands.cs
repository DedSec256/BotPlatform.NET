using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System;
using SimpleTelegramBotExample.Attributes;
using SimpleTelegramBotExample.Implementations;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleTelegramBotExample.Modules
{
	class SimpleCommands : CommandsModule<TelegramBotClient, Message>
	{
		public SimpleCommands() { }
		public override IEnumerable<(string Name, Callback<TelegramBotClient, Message> Callback)> Initialize()
		{
			return new (string, Callback<TelegramBotClient, Message>)[]
			{
				("/help", HelpCallback)
			};
		}

		[Log]
		private void HelpCallback(TelegramBotClient bot, Message message, object arg = null)
		{
			bot.SendTextMessageAsync(message.From.Id, $"Hello, user{message.From.Id}: ...");
		}

	}
	class SimpleCommands1 : CommandsModule<object, string>
	{ 
		public SimpleCommands1() { }
		public override IEnumerable<(string Name, Callback<object, string> Callback)> Initialize()
		{
			return new (string, Callback<object, string>)[]
			{
				("/help", HelpCallback)
			};
		}

		[Log]
		private void HelpCallback(object bot, string message, object arg = null)
		{
		}

	}
}
