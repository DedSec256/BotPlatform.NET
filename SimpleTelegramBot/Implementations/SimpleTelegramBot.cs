using System;
using BotPlatform.Core.System;
using BotPlatform.Core.Tools;
using BotPlatform.Core.Implementations.Bots;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleTelegramBotExample.Implementations
{
	class SimpleTelegramBot: Bot
	{
		public TelegramBotClient Instanse;
		public override bool Authorize()
		{
			try
			{
				Instanse = new TelegramBotClient("398931706:AAEUZBNEWcpEUK-qUbzB8gXBaNxYXvfQATk");
				Instanse.SetWebhookAsync("");
				return true;
			}
			catch { return false; }
		}
		protected override void StartBotWork()
		{ 
			Instanse.OnUpdate += Bot_OnUpdate;
			Instanse.StartReceiving();
		}
		private void Bot_OnUpdate(object sender, UpdateEventArgs e)
		{
			try
			{
				var update = e.Update;
				var message = update.Message;

				CommandCenter.Instance.Execute(Instanse, message, m => m.Text ?? "");
			}
			catch (Exception ex) { BotConsole.Write(ex); }
		}
	}
}
