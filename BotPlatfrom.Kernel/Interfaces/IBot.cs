using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface IBot
	{
		/// <summary>
		/// Авторизация бота
		/// </summary>
		/// <returns></returns>
		bool Autorize();
	}
}
