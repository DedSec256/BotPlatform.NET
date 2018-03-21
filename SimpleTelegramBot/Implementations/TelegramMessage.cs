using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace SimpleTelegramBotExample.Implementations
{
	public class TMessage : IMessage
	{
		public readonly Telegram.Bot.Types.Message Instance;

		public TMessage(Telegram.Bot.Types.Message m)
		{
			Instance = m;
		}

		public string Text
		{
			get { return Instance.Text; }

			set { }
		}

		public long UserId
		{
			get { return Instance.From.Id; }

			set { }
		}
	}
}
