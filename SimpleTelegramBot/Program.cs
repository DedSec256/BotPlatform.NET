using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;
using BotPlatfrom.Kernel.Tools;

namespace SimpleTelegramBotExample
{
	class Program
	{
		static void Main(string[] args)
		{
			BotConsole.Writer = (s, type) => { Console.Write(s); };

			SimpleTelegramBot bot = new SimpleTelegramBot();
			bot.StartWork();

			Console.ReadKey();
		}
	}
}
