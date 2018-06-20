using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System.Attributes;
using BotPlatfrom.Kernel.Tools;

namespace BotPlatfrom.Kernel.System
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
				if (InstanceHolder != null) return InstanceHolder;

				InstanceHolder = new CommandCenter();
				InstanceHolder.ExecuteModules();
				return InstanceHolder;
			}
		}

		protected Dictionary<string, dynamic> Commands;
		protected CommandCenter()
		{
			Commands = new Dictionary<string, dynamic>();
		}
		protected void ExecuteModules()
		{
			BotConsole.Write("[Подключение модулей...]\n", MessageType.System);
			/* Подключаем модули, создавая обьекты их классов */
			var typelist =
				Assembly.GetEntryAssembly().GetTypes()
					.Where(t => typeof(CommandsModule).IsAssignableFrom(t) &&
							    !t.GetCustomAttributes<IgnoreModuleAttribute>().Any())
					.OrderBy(t => t.FullName).ToArray();
			foreach (var type in typelist)
			{
				BotConsole.Write($"Подключение {type.FullName}...");

				dynamic module = Activator.CreateInstance(type);
				module.Initialize();

				BotConsole.Write($"Подключено.\n");
			}
			BotConsole.Write("Модули подключены.\n", MessageType.Info);
		}

		/// <summary>
		/// Добавление команды в систему
		/// </summary>
		/// <typeparam name="TBot">Тип бота, способного обрабатывать добавляемую команду</typeparam>
		/// <typeparam name="TMessage">Тип сообщения, способный обрабатываться ботом типа BotT</typeparam>
		/// <param name="commandName">Имя команды</param>
		/// <param name="callback">Обработчик команды</param>
		/// <returns>true, если команда с таким же названием не была добавлена ранее</returns>
		internal virtual bool TryAdd<TBot, TMessage>(string commandName, Callback<TBot, TMessage> callback)
		{
			if (Commands.ContainsKey(commandName)) return false;

			var decoratedCommand = callback.GetDecoratedCommand();
			Commands.Add(commandName, decoratedCommand);
			return true;
		}

		/// <summary>
		/// Асинхронный запуск обработки команды
		/// </summary>
		/// <param name="message">сообщени от пользователя</param>
		/// <param name="bot">бот, способный обработать команду</param>
		/// <param name="commandSelector"></param>
		/// <param name="arg">аргументы команды (полученные от бота или из сообщения)</param>
		/// <returns>true, если команда завершилась без необрабатываемых ошибок</returns>
		public virtual async Task<bool> ExecuteAsync<TBot, TMessage>
			(TBot bot, TMessage message, Func<TMessage, string> commandSelector, object arg = null)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(commandSelector(message), out var command))
			{
				return await command.ExecuteCommandAsync(bot, message, arg);
			}
			return false; /* true - если обработка успешна */
		}

		/// <summary>
		/// Запуск обработки команды
		/// </summary>
		/// <param name="message">сообщени от пользователя</param>
		/// <param name="bot">бот, способный обработать команду</param>
		/// <param name="commandSelector"></param>
		/// <param name="arg">аргументы команды (полученные от бота или из сообщения)</param>
		/// <returns>true, если команда завершилась без необрабатываемых ошибок</returns>
		public virtual bool Execute<TBot, TMessage>
			(TBot bot, TMessage message, Func<TMessage, string> commandSelector, object arg = null)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(commandSelector(message), out var command))
			{
				return command.ExecuteCommand(bot, message, arg);
			}
			return false; /* true - если команда завершилась без необрабатываемых ошибок */
		}
	}
}
