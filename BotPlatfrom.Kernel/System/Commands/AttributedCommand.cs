using System;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.System.Commands
{
	public abstract class AttributedCommand<TBot, TMessage> : Command<TBot, TMessage> 
	{
		protected Command<TBot, TMessage> BaseCommand;
		protected AttributedCommand(Command<TBot, TMessage> baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}

		protected sealed override bool Execute(TBot bot, TMessage message, object arg = null)
		{
			try
			{
				AttributedBeforeExecute(bot, message, arg);
				Callback(bot, message, arg);
				AttributedAfterExecute(bot, message, arg);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		protected sealed override Task<bool> ExecuteAsync(TBot bot, TMessage message, object arg = null)
		{
			return Task.Run(() => Execute(bot, message, arg));
		}

		protected void AttributedBeforeExecute(TBot bot, TMessage message, object arg = null)
		{
			BeforeExecute(bot, message, arg);
			BaseCommand.BeforeExecute(bot, message, arg);
		}
		protected void AttributedAfterExecute(TBot bot, TMessage message, object arg = null)
		{
			AfterExecute(bot, message, arg);
			BaseCommand.AfterExecute(bot, message, arg);
		}
	}
}