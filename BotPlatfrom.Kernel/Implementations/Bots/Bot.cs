using System;

namespace BotPlatfrom.Kernel.Implementations.Bots
{
	public abstract class Bot
	{
		protected bool IsAuthorized = false;
		public abstract bool Authorize();
		public void StartWork()
		{
			if (IsAuthorized) return;
			if (!Authorize())
				throw new InvalidOperationException("Не удалось авторизовать бота");
			StartBotWork();
		}
		protected abstract void StartBotWork();
	}
}
