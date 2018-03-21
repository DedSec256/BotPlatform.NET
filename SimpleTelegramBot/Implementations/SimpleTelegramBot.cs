using System;
using BotPlatfrom.Kernel.Command;
using BotPlatfrom.Kernel.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleTelegramBotExample.Implementations
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
				Instanse.OnUpdate += Bot_OnUpdate;
				Instanse.StartReceiving();
			}
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

				CommandCenter.Instance.Execute(message.FromTelegramMessage(), this);

			}
			catch (Exception ex) { }
		}
	}
}
