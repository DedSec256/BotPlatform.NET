using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Command.Attributes;
using BotPlatfrom.Kernel.Init;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.Tools;

namespace BotPlatfrom.Kernel.Command
{
	public class CommandCenter
	{
		protected static Lazy<CommandCenter> InstanceHolder =
			new Lazy<CommandCenter>(() => new CommandCenter());
		public static CommandCenter Instance => InstanceHolder.Value;

		protected Dictionary<string, Command> Commands;
		protected CommandCenter()
		{
			Commands = new Dictionary<string, Command>();
			ExecuteModules();
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
					Activator.CreateInstance(type);
					BotConsole.Write($"Подключено.\n");
				}
				catch
				{
					BotConsole.Write($"Ошибка при подключении модуля {type.FullName}\n", MessageType.Error);
				}
			}
			BotConsole.Write("Модули подключены.\n", MessageType.Info);
		}

		public virtual bool TryAdd<BotT>(string commandName, Callback<BotT> callback)
			where BotT: class, ISingleBot 
		{
			if (Commands.ContainsKey(commandName)) return false;

			var decoratedCommand = callback.GetCommand();
			Commands.Add(commandName, decoratedCommand);
			return true;
		}

		public virtual async Task<bool> ExecuteAsync(IMessage message, ISingleBot singleBot, object arg)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.BotType.IsInstanceOfType(singleBot))
				{
					return await command.ExecuteAsync(message, singleBot, arg);
				}
				else return false;
			}					
			else return false; /* true - если обработка успешна */
		}
		public virtual bool Execute(IMessage message, ISingleBot singleBot, object arg)
		{
			/* Проверяет, есть ли команда в системе */
			if (Commands.TryGetValue(message.Text, out Command command))
			{
				/* Проверяет, может ли бот данного типа выполнять эту команду */
				if (command.BotType.IsInstanceOfType(singleBot))
				{
					return command.Execute(message, singleBot, arg);
				}
				else return false;
			}
			else return false; /* true - если обработка успешна */
		}
	}
}
