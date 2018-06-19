using System;

namespace BotPlatfrom.Kernel.Command.Attributes
{
	/// <summary>
	/// Атрибут, помечающий модули команд, которые не должны добавляться в систему
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class IgnoreModuleAttribute : Attribute
	{
	}
}
