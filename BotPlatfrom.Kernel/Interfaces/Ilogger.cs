using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface ILogger
	{
		void LogInfo(string message);
		void LogWarning(string message);
		void LogError(string error);
		void LogError(Exception ex);
	}
}
