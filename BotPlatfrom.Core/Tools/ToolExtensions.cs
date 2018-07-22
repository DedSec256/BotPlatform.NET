using System;

namespace BotPlatform.Core.Tools
{
	internal static class ToolExtensions
	{
		internal static void IfNotNull<T>(this T obj, Action<T> action) where T : class
		{
			if (obj != null) action(obj);
		}
	}
}
