using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;
using BotPlatfrom.Kernel.IO;

namespace BotPlatfrom.Kernel.Init
{
	public static class SystemInitializer
	{
		/// <summary>
		/// Функция, подключающая все модули бота
		/// </summary>
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

		private static void InitalizeConsole(Action<string, MessageType> writer)
		{
			BotConsole.SetWriter((text, type) =>
			{
				if (type == MessageType.Error)
					Console.ForegroundColor = ConsoleColor.Red;
				else if (type == MessageType.Warning) Console.ForegroundColor = ConsoleColor.Yellow;
				else if (type == MessageType.Info) Console.ForegroundColor = ConsoleColor.Green;
				else if (type == MessageType.System) Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine(text);
				Console.ResetColor();
			});
			BotConsole.SetNotifyer((caption, text) =>
			{
				MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				BotConsole.Write($"{caption}: {text}", MessageType.Warning);
			});
		}
	}
}
