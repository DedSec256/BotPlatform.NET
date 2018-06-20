using System;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.System.Commands
{
	public class Command<TBot, TMessage>
	{
		public Type BotType     = typeof(TBot);
		public Type MessageType = typeof(TMessage);
		public readonly Callback<TBot, TMessage> Callback;

		public virtual void BeforeExecute(TBot bot, TMessage message, object arg = null){ }
		public virtual void AfterExecute(TBot bot, TMessage message, object arg = null) { }
		public Task<bool> ExecuteCommandAsync(dynamic bot, dynamic message, object arg = null)
		{
			return CanBeExecutedBy(bot, message) && ExecuteAsync(bot, message, arg);
		}
		public bool ExecuteCommand(dynamic bot, dynamic message, object arg = null)
		{
			return CanBeExecutedBy(bot, message) && Execute(bot, message, arg);
		}
		public virtual bool CanBeExecutedBy(object bot, object message)
		{
			return BotType.IsInstanceOfType(bot) && MessageType.IsInstanceOfType(message);
		}
		protected virtual Task<bool> ExecuteAsync(TBot bot, TMessage message, object arg = null)
		{
			return Task.Run(() =>
			{
				try
				{
					BeforeExecute(bot, message, arg);
					Callback(bot, message, arg);
					AfterExecute(bot, message, arg);

					return true;
				}
				catch (Exception ex)
				{
					return false;
				}
			});
		}
		protected virtual bool Execute(TBot bot, TMessage message, object arg = null)
		{
			try
			{
				BeforeExecute(bot, message, arg);
				Callback(bot, message, arg);
				AfterExecute(bot, message, arg);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Command(Callback<TBot, TMessage> callback)
		{
			Callback = callback;
		}
	}
}
