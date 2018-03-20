using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

		public virtual bool TryAdd<T>(string commandName, Callback callback) where T : IBot
		{
			if (Commands.ContainsKey(commandName)) return false;

			Commands.Add(commandName, new Command(callback, typeof(T)));
			return true;
		}
		public virtual bool TryAdd(string commandName, Callback callback) 
		{
			if (Commands.ContainsKey(commandName)) return false;

			Commands.Add(commandName, new Command(callback, typeof(IBot)));
			return true;
		}

		public virtual async Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg)
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
		public virtual bool Execute(IMessage message, IBot bot, object arg)
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
