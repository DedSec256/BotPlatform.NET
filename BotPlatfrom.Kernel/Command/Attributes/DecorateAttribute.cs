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
	public abstract class DecorateAttribute : Attribute
	{
		/// <summary>
		/// Возвращает обёрнутую команду
		/// в теле вызывает: return new MyAttributedCommand(command);
		/// </summary>
		/// <typeparam name="BotT">Тип бота, способного обрабатывать добавляемую команду</typeparam>
		/// <typeparam name="MessageT">Тип сообщения, способный обрабатываться ботом типа BotT</typeparam>
		/// <param name="command">Возвращает обёрнутую команду типа (BotT, MessageT)</param>
		/// <returns></returns>
		public abstract AttributedCommand<BotT, MessageT> Decorate<BotT, MessageT>(Command<BotT, MessageT> command)
			where BotT : class, IBot
			where MessageT : class, IMessage;
	}
}
