using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System;
using BotPlatfrom.Kernel.Tools;
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
