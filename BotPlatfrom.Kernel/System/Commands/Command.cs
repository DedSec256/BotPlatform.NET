using System;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.System.Commands
{
	public class Command<TBot, TMessage>
	{
		/// <summary>
		/// Тип бота 
		/// </summary>
		public readonly Type BotType     = typeof(TBot);
		/// <summary>
		/// Тип сообщений, принимаемого ботом
		/// </summary>
		public readonly Type MessageType = typeof(TMessage);
		/// <summary>
		/// Обработчик команды
		/// </summary>
		private readonly Callback<TBot, TMessage> _callback;
		/// <summary>
		/// Проверка возможности выполнения команды
		/// </summary>
		/// <param name="bot"></param>
		/// <param name="message"></param>
		/// <returns>true, если bot может выполнить команду из сообщения message</returns>
		public bool CanBeExecutedBy(object bot, object message)
		{
			return BotType.IsInstanceOfType(bot) && MessageType.IsInstanceOfType(message);
		}
		/// <summary>
		/// Запуск обработки команды
		/// </summary>
		/// <param name="bot"></param>
		/// <param name="message"></param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public bool ExecuteCommand(TBot bot, TMessage message, object arg = null)
		{
			return Execute(bot, message, arg);
		}
		protected virtual bool Execute(TBot bot, TMessage message, object arg = null)
		{
			try
			{
				_callback(bot, message, arg);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Command(Callback<TBot, TMessage> callback)
		{
			_callback = callback;
		}

		protected Command() { }
	}
}
