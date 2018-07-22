using System;
using BotPlatform.Core.System;
using BotPlatform.Core.Tools;
using SimpleTelegramBotExample.Implementations;

namespace SimpleTelegramBotExample
{
	class Program
	{
		static void Main(string[] args)
		{
			BotConsole.Writer = (s, type) => { Console.Write(s); };

			/* Включаем рефлексивную сборку модулей */
			CommandCenter.Instance.EnableAutoAssemble = true;

			var bot = new SimpleTelegramBot();
			bot.StartWork();

			Console.ReadKey();
		}
	}
}
