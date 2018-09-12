using System;
using System.Collections.Generic;
using BotPlatform.Core.Implementations.Attributes;
using BotPlatform.Core.System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.CommandModules
{
	internal class SimpleCommands : ICommandsModule<TelegramBotClient, Message>
	{      
		public IDictionary<string, Callback<TelegramBotClient, Message>> Initialize()
		{
			return new Dictionary<string, Callback<TelegramBotClient, Message>>()
			{
				{"/help", HelpCallback}
			};
		}

		[Log("здесь можно добавить текст логирования")]
		private static void HelpCallback(TelegramBotClient bot, Message message, object arg = null)
		{
			throw new NotImplementedException();
		}

	}
}
