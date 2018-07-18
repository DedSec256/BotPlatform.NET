using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Tools
{
	public interface IAsyncCommandExecutor
	{
		Task<bool> Run<TBot, TMessage>(Func<TBot, TMessage, Func<TMessage, string>, object, bool> func);
	}
}
