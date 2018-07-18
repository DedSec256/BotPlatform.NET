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
		private bool _enableAutoAssemble;
		private bool _isAssembled;
		public bool EnableAutoAssemble
		{
			get => _enableAutoAssemble;
			set
			{
				_enableAutoAssemble = value;
				if(_enableAutoAssemble && !_isAssembled)
					ExecuteModules();
			}
		}
		/// <summary>
		/// Статический экземпляр менеджера команд
		/// </summary>
		public static CommandCenter Instance
		{
			get
			{
				if (InstanceHolder != null) return InstanceHolder;

				InstanceHolder = new CommandCenter();
				return InstanceHolder;
			}
		}

		public static IAsyncCommandExecutor AsyncCommandExecutor { get; set; } = new AsyncCommandExecutor();

		protected static CommandCenter InstanceHolder;
		protected Dictionary<string, dynamic> Commands;
		protected CommandCenter()
		{
			Commands = new Dictionary<string, dynamic>();
		}
		protected void ExecuteModules()
		{
			BotConsole.Write("[Подключение модулей...]\n", MessageType.System);
			var assembly = Assembly.GetEntryAssembly();

			assembly.IfNotNull(a =>
			{
				var typelist = assembly.GetTypes()
					.Where(t => t.GetInterface(typeof(ICommandsModule<,>).FullName) != null &&
					            !t.GetCustomAttributes<IgnoreModuleAttribute>().Any())
					.OrderBy(t => t.FullName).ToArray();
				foreach (var type in typelist)
				{
					BotConsole.Write($"Подключение {type.FullName}...");
					dynamic module = Activator.CreateInstance(type);
					AddModule(module);
					BotConsole.Write($"Подключено.\n");
				}
			});
			BotConsole.Write("Модули подключены.\n", MessageType.Info);
			_isAssembled = true;
		}

		/// <summary>
		/// Добавляет модуль команд в систему
		/// </summary>
		/// <typeparam name="TBot"></typeparam>
		/// <typeparam name="TMessage"></typeparam>
		/// <param name="module"></param>
		public void AddModule<TBot, TMessage>(ICommandsModule<TBot, TMessage> module)
		{
			var result = module.Initialize();

			result.IfNotNull(commands =>
			{
				foreach (var command in commands)
				{
					if (!TryAdd(command.Key, command.Value))
					{
						throw new InvalidOperationException($"Команда с ключём {command} уже существует");
					}
				}
			});
		}

		public bool ContainsCommand(string command)
		{
			return Commands.ContainsKey(command);
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
		public async Task<bool> ExecuteAsync<TBot, TMessage>
			(TBot bot, TMessage message, Func<TMessage, string> commandSelector, object arg = null)
		{
			return await AsyncCommandExecutor.Run(() => Execute(bot, message, commandSelector, arg));
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
				if (command.CanBeExecutedBy(bot, message))
					return command.ExecuteCommand(bot, message, arg);
			}
			return false; 
		}
	}
}
