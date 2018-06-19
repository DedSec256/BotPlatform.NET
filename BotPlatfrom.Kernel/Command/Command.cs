using System;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public abstract class Command
	{
		public Type BotType     { get; protected set; }
		public Type MessageType { get; protected set; }
		protected abstract void BeforeExecute(dynamic message, dynamic bot, object arg = null);
		public    abstract Task<bool> ExecuteAsync(dynamic message, dynamic bot, object arg = null);
		public    abstract bool Execute(dynamic message, dynamic bot, object arg = null);
		protected abstract void AfterExecute(dynamic message, dynamic bot, object arg = null);
		public virtual bool CanBeExecutedBy(object bot, object message)
		{
			return BotType.IsInstanceOfType(bot) && MessageType.IsInstanceOfType(message);
		}
		protected Command(Type botType, Type messageType)
		{
			BotType     = botType;
			MessageType = messageType;
		}
	}

	public class Command<TBot, TMessage> : Command
	{
		public readonly Callback<TBot, TMessage> Callback;
		public sealed override Task<bool> ExecuteAsync(dynamic message, dynamic bot, object arg = null)
		{
			return ExecuteAsync(message, bot, arg);
		}
		public sealed override bool Execute(dynamic message, dynamic bot, object arg = null)
		{
			return Execute(message, bot, arg);
		}
		protected sealed override void BeforeExecute(dynamic message, dynamic bot, object arg = null)
		{
			BeforeExecute(message, bot, arg);
		}
		protected sealed override void AfterExecute(dynamic message, dynamic bot, object arg = null)
		{
			AfterExecute(message, bot, arg);
		}

		public virtual void BeforeExecute(TMessage message, TBot bot, object arg = null)
		{
		}
		public virtual void AfterExecute(TMessage message, TBot bot, object arg = null)
		{

		}
		public virtual Task<bool> ExecuteAsync(TMessage message, TBot bot, object arg = null)
		{
			return Task.Run(() =>
			{
				try
				{
					BeforeExecute(message, bot, arg);
					Callback(message, bot, arg);
					AfterExecute(message, bot, arg);

					return true;
				}
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
				catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
				{
					return false;
				}
			});
		}
		public virtual bool Execute(TMessage message, TBot bot, object arg = null)
		{
			try
			{
				BeforeExecute(message, bot, arg);
				Callback(message, bot, arg);
				AfterExecute(message, bot, arg);

				return true;
			}
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
			catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
			{
				return false;
			}
		}

		public Command(Callback<TBot, TMessage> callback) : base(typeof(TBot), typeof(TMessage))
		{
			Callback = callback;
		}
	}

	public abstract class AttributedCommand<TBot, TMessage> : Command<TBot, TMessage> 
	{
		protected Command<TBot, TMessage> BaseCommand;
		protected AttributedCommand(Command<TBot, TMessage> baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}

		public sealed override bool Execute(TMessage message, TBot bot, object arg = null)
		{
			try
			{
				AttributedBeforeExecute(message, bot, arg);
				Callback(message, bot, arg);
				AttributedAfterExecute(message, bot, arg);

				return true;
			}
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
			catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
			{
				return false;
			}
		}
		public sealed override Task<bool> ExecuteAsync(TMessage message, TBot bot, object arg = null)
		{
			return Task.Run(() =>
			{
				try
				{
					AttributedBeforeExecute(message, bot, arg);
					Callback(message, bot, arg);
					AttributedAfterExecute(message, bot, arg);

					return true;
				}
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
				catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
				{
					return false;
				}
			});
		}

		protected void AttributedBeforeExecute(TMessage message, TBot bot, object arg = null)
		{
			BeforeExecute(message, bot, arg);
			BaseCommand.BeforeExecute(message, bot, arg);
		}
		protected void AttributedAfterExecute(TMessage message, TBot bot, object arg = null)
		{
			AfterExecute(message, bot, arg);
			BaseCommand.AfterExecute(message, bot, arg);
		}
	}
}
