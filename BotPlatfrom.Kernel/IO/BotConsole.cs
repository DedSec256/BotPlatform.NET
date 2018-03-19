using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.IO
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
		static BotConsole()
		{
			Writer = null;
			Notifyer = null;
		}
		
		private static Action<string, MessageType> Writer;
		private static Action<string, string> Notifyer;

		public static void SetWriter(Action<string, MessageType> _writer)
		{
			Writer = _writer;
		}
		public static void SetNotifyer(Action<string, string> _notifyer)
		{
			Notifyer = _notifyer;
		}
		public static void Write(string text, MessageType type = MessageType.Default)
		{
			//lock(lockObj) Writer(text, type);
			Writer(text, type);
			Log.GetLog().Log(LogLevel.Info, text);
		}

		public static void Write(Exception ex)
		{
			string exMessage = "---------------------------\n" +
			                   ex.Message + "\nSTACK_TRACE: " + ex.StackTrace + "\n" +
			                   "---------------------------";
			//lock(lockObj) Writer(text, type);
			Writer(exMessage, MessageType.Error);
			Log.GetLog().Log(LogLevel.Error, exMessage);
		}

		internal static void StartReading()
		{
			while (true)
			{
				Reader();
			}
		}

		public static void Notify(string caption, string text)
		{
			Notifyer(caption, text);
		}
		public struct CommandInfo
		{
			public string Command { get; private set; }
			public string Param { get; private set; }

			public CommandInfo(string command, string param)
			{
				this.Command = command;
				this.Param = param;
			}
		}

		/*
        static CommandInfo GetCommandFromMessage(string consoleMessage)
        {
            int index = consoleMessage.IndexOf('(');

            string command = consoleMessage;
            string param = null;

            if (index != -1)
            {
                command = consoleMessage.Substring(0, index);
                param = consoleMessage.Substring(index + 1);
                int ind = param.LastIndexOf(')');
                if (ind == -1)
                {
                    param += ')';
                    param = param.Remove(param.Length - 1);
                }
                else param = param.Remove(ind);
            }
            command = command.Trim();

            if (!String.IsNullOrEmpty(command) &&
                Char.IsUpper(command[0])) command = Char.ToLower(command[0]) + command.Substring(1);
            return new CommandInfo(command, param);
        }
        */
	}
}
