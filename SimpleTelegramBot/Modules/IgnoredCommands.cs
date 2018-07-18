using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System;
using BotPlatfrom.Kernel.System.Attributes;

namespace SimpleTelegramBotExample.Modules
{
	/* Не добавляем модуль в сборку команд */
	[IgnoreModule]
	class IgnoredCommands : ICommandsModule<object, object>
	{
		public IgnoredCommands() { }
		public IDictionary<string, Callback<object, object>> Initialize()
		{
			/* Этот метод не вызовется */
			return null;
		}
	}
}
