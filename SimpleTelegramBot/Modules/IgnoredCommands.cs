using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command.Attributes;
using BotPlatfrom.Kernel.Interfaces;

namespace SimpleTelegramBotExample.Modules
{
	/* Не добавляем модуль в сборку команд */
	[IgnoreModule]
	class IgnoredCommands : ICommandsModule
	{
		public IgnoredCommands()
		{
			
		}
		public void Initialize()
		{
			/* Этот метод не вызовется */
		}
	}
}
