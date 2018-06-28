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
		public override IDictionary<string, Callback<TelegramBotClient, Message>> Initialize()
		{
			return new Dictionary<string, Callback<TelegramBotClient, Message>>()
			{
				{"/help", HelpCallback}
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
		public override IDictionary<string, Callback<object, string>> Initialize()
		{
			return new Dictionary<string, Callback<object, string>>()
			{
				{"/help", HelpCallback}
			};
		}

		[Log]
		private void HelpCallback(object bot, string message, object arg = null)
		{
		}

	}
}
