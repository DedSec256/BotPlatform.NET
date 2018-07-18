using System.Collections.Generic;
using BotPlatfrom.Kernel.System.Commands;

namespace BotPlatfrom.Kernel.System
{
	public abstract class CommandsModule
	{
		public virtual void Initialize() { }
	}
	public abstract class CommandsModule<TBot, TMessage> : CommandsModule
	{
		public abstract IDictionary<string, Callback<TBot, TMessage>> Initialize();
	}
}
