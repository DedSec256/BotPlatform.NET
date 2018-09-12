using BotPlatform.Core.System.Attributes;
using BotPlatform.Core.System.Commands;
using BotPlatform.Core.Tools;

namespace BotPlatform.Core.Implementations.Attributes
{
	/// <summary>
	/// Базовый атрибут для логирования
	/// </summary>
	public class LogAttribute : DecorateAttribute
	{
		private readonly string _logText;
		/// <summary>
		/// </summary>
		/// <param name="logText">Текст логирования</param>
		public LogAttribute(string logText)
		{
			_logText = logText;
		}

		public override AttributedCommand<TBot, TMessage> Decorate<TBot, TMessage>(Command<TBot, TMessage> command)
		{
			return new LogAttributedCommand<TBot, TMessage>(command, _logText);
		}
	}

	public class LogAttributedCommand<TBot, TMessage> : AttributedCommand<TBot, TMessage>
	{
		private readonly string _logText;

		public LogAttributedCommand(Command<TBot, TMessage> baseCommand, string logText = "") : base(baseCommand)
		{
			_logText = logText;
		}

		protected override bool Execute(TBot bot, TMessage message, object arg = null)
		{
			BotConsole.Write(_logText);
			return BaseCommand.ExecuteCommand(bot, message, arg);
		}
	}
}
