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
			CommandCenter.Instance.TryAdd<SimpleTelegramBot>("/help", HelpCallback);
			CommandCenter.Instance.TryAdd<SimpleTelegramBot, TMessage>("/start", StartCallback);
		}

		[Log]
		private void StartCallback(TMessage message, SimpleTelegramBot bot, object o)
		{
			bot.Instanse.SendTextMessageAsync(message.UserId, message.Instance.Caption);
		}

		[Log]
		private void HelpCallback(IMessage message, SimpleTelegramBot bot, object obj = null)
		{
			bot.Instanse.SendTextMessageAsync(message.UserId, $"Здравствуйте, user{message.UserId}: ...");
		}
	}
}
