using System.Collections.Generic;

namespace BotPlatform.Core.System
{
	public interface ICommandsModule<TBot, TMessage>
	{
		IDictionary<string, Callback<TBot, TMessage>> Initialize();
	}
}
