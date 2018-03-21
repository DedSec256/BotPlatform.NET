using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface ISingleBot
	{
		bool Autorize();
		void StartWork();
	}
	public interface IUniversalSingleBot : ISingleBot
	{
		Message SendTextMessage(string userId, string text);
		Message SendTextMessage(long userId, string text);
		/* ... */
	}
}
