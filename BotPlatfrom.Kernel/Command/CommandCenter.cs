using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command.Attributes;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.Tools;

namespace BotPlatfrom.Kernel.Command
{
	/// <summary>
	/// Менеджер команд
	/// </summary>
	public class CommandCenter
	{
		protected static CommandCenter InstanceHolder;
		/// <summary>
		/// Статический экземпляр менеджера команд
		/// </summary>
		public static CommandCenter Instance
		{
			get
			{
				if (InstanceHolder == null)
				{
					InstanceHolder = new CommandCenter();
					InstanceHolder.ExecuteModules();
				}
				return InstanceHolder;
			}
		}

		protected Dictionary<string, Command> Commands;
		protected CommandCenter()
		{
			Commands = new Dictionary<string, Command>();
		}
		private void ExecuteModules()
		{
			BotConsole.Write("[Подключение модулей...]\n", MessageType.System);
			/* Подключаем модули, создавая обьекты их классов */
			var typelist =
				Assembly.GetEntryAssembly().GetTypes()
					.Where(t => typeof(ICommandsModule).IsAssignableFrom(t)  
					   && !t.GetCustomAttributes<IgnoreModuleAttribute>().Any())
					.OrderBy(t => t.FullName).ToArray();
			foreach (var type in typelist)
			{
				BotConsole.Write($"Подключение {type.FullName}...");
				try
				{
					var module = Activator.CreateInstance(type) as ICommandsModule;
					module.Initialize();
					BotConsole.Write($"Подключено.\n");
				}
				catch
				{
					BotConsole.Write($"Ошибка при подключении модуля {type.FullName}\n", MessageType.Error);
				}
			}
			BotConsole.Write("Модули подключены.\n", MessageType.Info);
		}
		/// <summary>
		/// Добавление команды в систему
		/// </summary>
		/// <typeparam name="BotT">Тип бота, способного обрабатывать добавляемую команду</typeparam>
		/// <param name="commandName">Имя команды</param>
		/// <param name="callback">Обработчик команды</param>
		/// <returns>true, если команда с таким же названием не была добавлена ранее</returns>
		public virtual bool TryAdd<BotT>(string commandName, Callback<BotT, IMessage> callback)
			where BotT: class, IBot
		{
			return TryAdd<BotT, IMessage>(commandName, callback);
		}
		/// <summary>
		/// Добавление команды в систему
		/// </summary>
		/// <typeparam name="BotT">Тип бота, способного обрабатывать добавляемую команду</typeparam>
		/// <typeparam name="MessageT">Тип сообщения, способный обрабатываться ботом типа BotT</typeparam>
		/// <param name="commandName">Имя команды</param>
		/// <param name="callback">Обработчик команды</param>
		/// <returns>true, если команда с таким же названием не была добавлена ранее</returns>
		public virtual bool TryAdd<BotT, MessageT>(string commandName, Callback<BotT, MessageT> callback)
			where BotT : class, IBot
			where MessageT: class, IMessage
		{
			if (Commands.ContainsKey(commandName)) return false;

			var decoratedCommand = callback.GetCommand();
			Commands.Add(commandName, decoratedCommand);
			return true;
		}
		/// <summary>
		/// Асинхронный запуск обработки команды
		/// </summary>
		/// <param name="message">сообщени от пользователя</param>
		/// <param name="bot">бот, способный обработать команду</param>
		/// <param name="arg">аргументы команды (полученные от бота или из сообщения)</param>
		/// <returns>true, если команда завершилась без необрабатываемых ошибок</returns>
		public virtual async Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.CanBeExecutedBy(bot, message))
				{
					return await command.ExecuteAsync(message, bot, arg);
				}
				else return false;
			}					
			else return false; /* true - если обработка успешна */
		}
		/// <summary>
		/// Запуск обработки команды
		/// </summary>
		/// <param name="message">сообщени от пользователя</param>
		/// <param name="bot">бот, способный обработать команду</param>
		/// <param name="arg">аргументы команды (полученные от бота или из сообщения)</param>
		/// <returns>true, если команда завершилась без необрабатываемых ошибок</returns>
		public virtual bool Execute(IMessage message, IBot bot, object arg = null)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.CanBeExecutedBy(bot, message))
				{
					return command.Execute(message, bot, arg);
				}
				else return false;
			}
			else return false; /* true - если обработка успешна */
		}
	}
}
