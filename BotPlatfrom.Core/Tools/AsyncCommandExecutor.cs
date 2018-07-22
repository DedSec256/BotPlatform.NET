using System;
using System.Threading.Tasks;

namespace BotPlatform.Core.Tools
{
	
	public class AsyncCommandExecutor : IAsyncCommandExecutor
	{
		public Task<bool> Run(Func<bool> func)
		{
			return Task.Run(func);
		}
	}
	
}
