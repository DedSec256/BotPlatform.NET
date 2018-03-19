using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Interfaces
{
	public interface ILogger
	{
		void LogInfo<T>(T message);
		void LogWarning<T>(T message);
		void LogError<T>(T error);
		void LogError(Exception ex);
	}
}
