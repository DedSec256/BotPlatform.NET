using System;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using BotPlatfrom.Kernel.System;

namespace BotPlatfrom.Kernel.Communication
{
	/*
	public class User
	{
		public readonly string Id;
		protected (Regex contextPredicate, Callback<,> Done, Callback Failed) Context;

		public User(string id)
		{
			Id = id;
		}
		//Устанавливает контекст по умолчанию
	
		public virtual void SetDefaultContext()
		{
			Context = (contextPredicate: new Regex(".*"),  // Принимает любые сообщения   
					   Done: async (message, bot, arg) =>
					   {
							// Запускает обработку команды  
							await CommandCenter.Instance.ExecuteAsync(message, bot, arg);
					   }, 
					   Failed: (message, bot, arg) => { });
		}
		public virtual void SetContext(Regex regex, Callback onDone, Callback onFailed)
		{
			Context = (contextPredicate: regex, Done: onDone, Failed: onFailed);
		}
		 // Бот при получении нового сообщения от пользователя передаёт его сюда 
		public virtual void ExecuteCurrentContext<TBot, TMessage>(TBot bot, TMessage message, Func<TMessage, string> commandSelector, object arg)
		{
			// Проверка, соответствует ли сообщение контексту 
			var command = commandSelector(message);
			if (Context.contextPredicate.Match(command).Value == command)
			{
				Context.Done(bot, message, arg);
			}
			else
			{
				Context.Failed(bot, message, arg);
			}
		}
	}*/
}
