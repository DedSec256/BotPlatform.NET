using System;

namespace BotPlatform.Core.Tools
{
	public interface ILogger
	{
		void LogInfo(string message);
		void LogWarning(string message);
		void LogError(string error);
		void LogError(Exception ex);
	}
}
