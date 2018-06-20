using System;
using BotPlatfrom.Kernel.Implementations;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.System;
using BotPlatfrom.Kernel.Tools;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace SimpleTelegramBotExample.Implementations
{
	class SimpleTelegramBot: Bot
	{
		public TelegramBotClient Instanse;
		public override bool Authorize()
		{
			try
			{
				Instanse = new TelegramBotClient("AuthToken");
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

				CommandCenter.Instance.Execute(Instanse, message, m => m.Text);
			}
			catch (Exception ex) { BotConsole.Write(ex); }
		}
	}
}
