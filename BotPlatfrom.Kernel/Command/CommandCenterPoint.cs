using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public class CommandCenterPoint<TBot, TMessage>
	{
		private readonly CommandCenter _instance = CommandCenter.Instance;

		public virtual bool TryAdd(string commandName, Callback<TBot, TMessage> callback)
		{
			return _instance.TryAdd<TBot, TMessage>(commandName, callback);
		}
	}
}
