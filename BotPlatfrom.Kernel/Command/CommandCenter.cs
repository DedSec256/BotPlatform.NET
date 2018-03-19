using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

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
