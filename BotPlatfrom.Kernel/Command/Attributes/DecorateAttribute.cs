using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command.Attributes
{
	/// <summary>
	/// Атрибут, позволяющий оборачивать функцию-обработчик команды
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class DecorateAttribute : Attribute
	{
		/// <summary>
		/// Возвращает обёрнутую команду
		/// в теле вызывает: return new MyAttributedCommand(command);
		/// </summary>
		/// <typeparam name="TBot">Тип бота, способного обрабатывать добавляемую команду</typeparam>
		/// <typeparam name="TMessage">Тип сообщения, способный обрабатываться ботом типа BotT</typeparam>
		/// <param name="command">Возвращает обёрнутую команду типа (BotT, MessageT)</param>
		/// <returns></returns>
		public abstract AttributedCommand<TBot, TMessage> Decorate<TBot, TMessage>(Command<TBot, TMessage> command);
	}
}
