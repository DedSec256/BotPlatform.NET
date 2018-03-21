using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;
using Telegram.Bot;

namespace SimpleTelegramBotExample
{
	class SimpleTelegramBot: ISingleBot
	{
		public TelegramBotClient Instanse;
		private bool _autorized = false;
		public SimpleTelegramBot()
		{
			_autorized = Autorize();
		}
		public bool Autorize()
		{
			try
			{
				Instanse = new TelegramBotClient("398931706:AAEUZBNEWcpEUK-qUbzB8gXBaNxYXvfQATk");
				Instanse.SetWebhookAsync("");
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void StartWork()
		{
			if (_autorized)
			{
				
			}
		}
	}
}
