using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Tools
{
	public interface IAsyncCommandExecutor
	{
		Task<bool> Run(Func<bool> func);
	}
}
