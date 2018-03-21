using BotPlatfrom.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using SimpleTelegramBotExample.Attributes;

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
		private void HelpCallback(IMessage message, SimpleTelegramBot bot, object obj = null)
		{
			Console.WriteLine("help: ...");
		}
	}
}
