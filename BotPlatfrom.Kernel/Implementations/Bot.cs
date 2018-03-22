using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Implementations
{
	public abstract class Bot : IBot
	{
		protected bool IsAutorized = false;
		public abstract bool Autorize();

		public void StartWork()
		{
			if(!IsAutorized && !Autorize())
				throw new InvalidOperationException("Не удалось авторизовать бота");
			StartBotWork();	
		}
		protected abstract void StartBotWork();
	}
}
