using System;

namespace BotPlatform.Core.Implementations.Bots
{
	/// <summary>
	/// Пример базового класса бота
	/// </summary>
	public abstract class Bot
	{
		/// <summary>
		/// Прошёл ли бот авторизацию
		/// </summary>
		public bool IsAuthorized { get; protected set; }
		/// <summary>
		/// Определите аторизацию бота тут
		/// </summary>
		/// <returns></returns>
		public abstract bool Authorize();
		public void StartWork()
		{
			if (IsAuthorized) return;
			if (!Authorize())
				throw new InvalidOperationException("Не удалось авторизовать бота");
			StartBotWork();
		}
		/// <summary>
		/// Определите поведение бота после авторизации тут
		/// </summary>
		protected abstract void StartBotWork();
	}
}
