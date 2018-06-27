using System;

namespace BotPlatfrom.Kernel.Tools
{
	public enum MessageType
	{
		Error,
		Info,
		Warning,
		System,
		Default
	}

	public static class BotConsole
	{
		public static ILogger             LoggerInstance { get; set; }
		public static Action<string, MessageType> Writer { get; set; }
		public static void Write(string text, MessageType type = MessageType.Default)
		{
			Writer?.Invoke(text, type);
			LoggerInstance?.LogInfo(text);
		}

		public static void Write(Exception ex)
		{
			string exMessage = ex.Message + "\nSTACK_TRACE: " + ex.StackTrace;
			Writer?.Invoke(exMessage, MessageType.Error);
			LoggerInstance?.LogError(exMessage);
		}

	}
}
