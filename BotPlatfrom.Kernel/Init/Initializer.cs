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
	public static class CommandsInitializer
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
	}
}
