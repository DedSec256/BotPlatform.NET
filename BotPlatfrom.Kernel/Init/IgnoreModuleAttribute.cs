using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Init
{
	/// <summary>
	/// Атрибут, помечающий модули команд, которые не должны добавляться в систему
	/// </summary>
	public class IgnoreModuleAttribute : Attribute
	{
	}
}
