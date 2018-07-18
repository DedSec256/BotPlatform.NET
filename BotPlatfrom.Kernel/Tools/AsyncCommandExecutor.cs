using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Tools
{
	
	public class AsyncCommandExecutor : IAsyncCommandExecutor
	{
		public Task<bool> Run(Func<bool> func)
		{
			return Task.Run(func);
		}
	}
	
}
