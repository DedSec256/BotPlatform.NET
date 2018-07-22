using System.Collections.Generic;
using BotPlatform.Core.System;
using BotPlatform.Core.System.Attributes;
using BotPlatform.Core.Implementations.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleTelegramBotExample.Modules
{
	class SimpleCommands : ICommandsModule<TelegramBotClient, Message>
	{      
		public IDictionary<string, Callback<TelegramBotClient, Message>> Initialize()
		{
			return new Dictionary<string, Callback<TelegramBotClient, Message>>()
			{
				{"/help", HelpCallback}
			};
		}

		[Log("HelpTest")]
		private void HelpCallback(TelegramBotClient bot, Message message, object arg = null)
		{
			bot.SendTextMessageAsync(message.From.Id, $"Hello, user{message.From.Id}: ...");
		}

	}

	[IgnoreModule]
	class SimpleCommands1 : ICommandsModule<object, string>
	{ 
		public IDictionary<string, Callback<object, string>> Initialize()
		{
			return new Dictionary<string, Callback<object, string>>()
			{
				{"/help", HelpCallback}
			};
		}

		[Log]
		private void HelpCallback(object bot, string message, object arg = null)
		{ }

	}
}
