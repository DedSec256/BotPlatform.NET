using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command;

namespace BotPlatfrom.Kernel.Interfaces
{
	public class User
	{
		public readonly string Id;
		protected (Regex contextPredicate, Callback Done, Callback Failed) Context;
		/* Устанавливает контекст по умолчанию */
		public virtual void SetDefaultContext()
		{
			Context = (contextPredicate: new Regex(".*"), /* Принимает любые сообщения */
					   Done: async (message, bot, arg) =>
					   {
							/* Запускает обработку команды */
							await CommandCenter.Instance.ExecuteAsync(message, bot, arg);
					   }, 
					   Failed: (message, bot, arg) => { });
		}
		public virtual void SetContext(Regex regex, Callback done, Callback failed)
		{
			Context = (contextPredicate: regex, Done: done, Failed: failed);
		}
		/* Бот при получении нового сообщения от пользователя передаёт его сюда */
		public void ExecuteCurrentContext(IMessage message, IBot bot, object arg)
		{
			/* Проверка, соответствует ли сообщение контексту */
			if (Context.contextPredicate.Match(message.Text).Value == message.Text)
			{
				Context.Done(message, bot, arg);
			}
			else
			{
				Context.Failed(message, bot, arg);
			}
		}
	}
}
