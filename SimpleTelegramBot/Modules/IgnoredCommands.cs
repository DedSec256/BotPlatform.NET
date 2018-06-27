using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.System;
using BotPlatfrom.Kernel.System.Attributes;

namespace SimpleTelegramBotExample.Modules
{
	/* Не добавляем модуль в сборку команд */
	[IgnoreModule]
	class IgnoredCommands : CommandsModule<object, object>
	{
		public IgnoredCommands() { }
		public override IEnumerable<(string Name, Callback<object, object> Callback)> Initialize()
		{
			/* Этот метод не вызовется */
			return null;
		}
	}
}
