using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace SimpleTelegramBotExample.Implementations
{
	public static class MessageExtension
	{
		public static Message FromTelegramMessage(this Telegram.Bot.Types.Message message)
		{
			return new Message()
			{
				Caption = message.Caption,
				Date = message.Date,
				MessageId = message.MessageId,
				Text = message.Text,
				UserId = message.From.Id
			};
		}
	}
}
