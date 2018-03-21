using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface IBot
	{
		bool Autorize();
		void StartWork();
	}
	public interface IUniversalBot : IBot
	{
		IMessage SendTextMessage(string userId, string text);
		IMessage SendTextMessage(long userId, string text);
		/* ... */
	}
}
