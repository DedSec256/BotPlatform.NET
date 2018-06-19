using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;

namespace BotPlatfrom.Kernel.Interfaces
{
	public abstract class CommandsModule<TBot, TMessage>
	{
		protected readonly CommandCenterPoint<TBot, TMessage> CommandCenter = new CommandCenterPoint<TBot, TMessage>();
		public abstract void Initialize();
	}
}
