using System.Collections.Generic;

namespace BotPlatfrom.Kernel.System
{
	public interface ICommandsModule<TBot, TMessage>
	{
		IDictionary<string, Callback<TBot, TMessage>> Initialize();
	}
}
