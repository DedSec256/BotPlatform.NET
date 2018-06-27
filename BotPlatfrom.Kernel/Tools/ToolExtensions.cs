using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Tools
{
	internal static class ToolExtensions
	{
		internal static void IfNotNull<T>(this T obj, Action<T> action) where T : class
		{
			if(obj != null) action(obj);
		}
	}
}
