using System;
using BotPlatform.Core.System;
using BotPlatform.Core.Tools;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
	internal class SimpleTelegramBot: BotPlatform.Core.Implementations.Bots.Bot
	{
		public TelegramBotClient BotClient;
		public override bool Authorize()
		{
			try
			{
				BotClient = new TelegramBotClient("398931706:AAEUZBNEWcpEUK-qUbzB8gXBaNxYXvfQATk");
				BotClient.SetWebhookAsync("");
				return true;
			}
			catch { return false; }
		}
		protected override void StartBotWork()
		{ 
			BotClient.OnUpdate += Bot_OnUpdate;
			BotClient.StartReceiving();
		}
		private void Bot_OnUpdate(object sender, UpdateEventArgs e)
		{
			try
			{
				var update = e.Update;
				var message = update.Message;

				CommandCenter.Instance.Execute(BotClient, message, m => m.Text ?? "");
			}
			catch (Exception ex) { BotConsole.Write(ex); }
		}
	}
}
