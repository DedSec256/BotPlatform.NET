using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Init;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.IO;

namespace BotPlatfrom.Kernel.Command
{
	class CommandCenter
	{
		private static readonly Lazy<CommandCenter> instanceHolder =
			new Lazy<CommandCenter>(() => new CommandCenter());
		public static CommandCenter Instance
		{
			get { return instanceHolder.Value; }
		}

		private Dictionary<string, Command> Commands;
		public CommandCenter()
		{
			Commands = new Dictionary<string, Command>();
			ExecuteModules();
		}
		static void ExecuteModules()
		{
			BotConsole.Write("[Подключение модулей...]", MessageType.System);

			/* Подключаем модули, создавая обьекты их классов */
			String[] typelist =
				Assembly.GetExecutingAssembly().GetTypes()
					.Where(t => typeof(ICommandsModule).IsAssignableFrom(t) &&
					            !t.IsInterface && !t.GetCustomAttributes<IgnoreModuleAttribute>().Any())
					.OrderBy(t => t.FullName)
					.Select(t => t.FullName).ToArray();
			foreach (var type in typelist)
			{
				Activator.CreateInstance(Type.GetType(type));
				BotConsole.Write("Подключение " + type + "...");
			}
			BotConsole.Write("Модули подключены.", MessageType.Info);
		}

		public bool TryAdd<T>(string commandName, Callback callback) where T : IBot
		{
			if (Commands.ContainsKey(commandName)) return false;

			Commands.Add(commandName, new Command(callback, typeof(T)));
			return true;
		}

		public async Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.BotType.IsInstanceOfType(bot))
				{
					return await command.ExecuteAsync(message, bot, arg);
				}
				else return false;
			}					
			else return false; /* true - если обработка успешна */
		}
		public bool Execute(IMessage message, IBot bot, object arg)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.BotType.IsInstanceOfType(bot))
				{
					return command.Execute(message, bot, arg);
				}
				else return false;
			}
			else return false; /* true - если обработка успешна */
		}
	}
}
