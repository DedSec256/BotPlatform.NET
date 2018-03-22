using System;
using BotPlatfrom.Kernel.Command;
using BotPlatfrom.Kernel.Implementations;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.Tools;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleTelegramBotExample.Implementations
{
	class SimpleTelegramBot: Bot
	{
		public TelegramBotClient Instanse;
		public override bool Autorize()
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

		protected override void StartBotWork()
		{
			Instanse.OnUpdate += Bot_OnUpdate;
			Instanse.StartReceiving();
		}

		private void Bot_OnUpdate(object sender, UpdateEventArgs e)
		{
			try
			{
				if (e.Update.CallbackQuery != null || e.Update.InlineQuery != null)
					return;
				var update = e.Update;
				var message = update.Message;
				if (message == null) return;

				CommandCenter.Instance.Execute(new TMessage(message), this);
			}
			catch (Exception ex)
			{
				BotConsole.Write(ex);
			}
		}
	}
}
