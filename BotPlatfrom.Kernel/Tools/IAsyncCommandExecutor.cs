using System;
using System.Threading.Tasks;

namespace BotPlatform.Core.Tools
{
	public interface IAsyncCommandExecutor
	{
		Task<bool> Run(Func<bool> func);
	}
}
