using System;

namespace BotPlatform.Core.System.Attributes
{
	/// <summary>
	/// Атрибут, помечающий модули команд, которые не должны добавляться в систему
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class IgnoreModuleAttribute : Attribute
	{ }
}
