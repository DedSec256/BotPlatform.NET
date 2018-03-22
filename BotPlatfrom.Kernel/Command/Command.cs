using System;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public abstract class Command
	{
		public Type BotType { get; protected set; } = typeof(IBot);
		public Type MessageType { get; protected set; } = typeof(IMessage);
		protected abstract void BeforeExecute(IMessage message, IBot bot, object arg = null);
		public abstract Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg = null);
		public abstract bool Execute(IMessage message, IBot bot, object arg = null);
		protected abstract void AfterExecute(IMessage message, IBot bot, object arg = null);
		public bool CanBeExecutedBy(IBot bot, IMessage message = null)
		{
			if (message == null)
				return BotType.IsInstanceOfType(bot);
			return BotType.IsInstanceOfType(bot) && MessageType.IsInstanceOfType(message);
		}

	}

	public class Command<BotT, MessageT> : Command
		where BotT : class, IBot 
		where MessageT: class, IMessage
	{
		public readonly Callback<BotT, MessageT> Callback;
		public sealed override Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg = null)
		{
			var messageT = message as MessageT;
			if (bot is BotT botT && messageT != null) return ExecuteAsync(messageT, botT, arg);

			throw new InvalidCastException($"Cannot convert type from {message.GetType()} or from {bot.GetType()}");
		}
		public sealed override bool Execute(IMessage message, IBot bot, object arg = null)
		{
			var messageT = message as MessageT;
			if (bot is BotT botT && messageT != null) return Execute(messageT, botT, arg);

			throw new InvalidCastException($"Cannot convert type from {message.GetType()} or from {bot.GetType()}");
		}
		protected sealed override void BeforeExecute(IMessage message, IBot bot, object arg = null)
		{
			var messageT = message as MessageT;
			if (bot is BotT botT && messageT != null) BeforeExecute(messageT, botT, arg);

			throw new InvalidCastException($"Cannot convert type from {message.GetType()} or from {bot.GetType()}");
		}
		protected sealed override void AfterExecute(IMessage message, IBot bot, object arg = null)
		{
			var messageT = message as MessageT;
			if (bot is BotT botT && messageT != null) AfterExecute(messageT, botT, arg);

			throw new InvalidCastException($"Cannot convert type from {message.GetType()} or from {bot.GetType()}");
		}

		public virtual void BeforeExecute(MessageT message, BotT bot, object arg = null)
		{

		}
		public virtual void AfterExecute(MessageT message, BotT bot, object arg = null)
		{

		}
		public virtual Task<bool> ExecuteAsync(MessageT message, BotT bot, object arg = null)
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
		public virtual bool Execute(MessageT message, BotT bot, object arg = null)
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

		public Command(Callback<BotT, MessageT> callback)
		{
			Callback = callback;
			BotType = typeof(BotT);
		}
	}

	public class Command<BotT> : Command<BotT, IMessage> where BotT : class, IBot
	{
		protected Command(Callback<BotT, IMessage> callback) : base(callback)
		{
		}
	}

	public abstract class AttributedCommand<BotT, MessageT> : Command<BotT, MessageT> 
		where BotT : class, IBot
		where MessageT: class, IMessage
	{
		protected Command<BotT, MessageT> BaseCommand;
		protected AttributedCommand(Command<BotT, MessageT> baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}

		public sealed override bool Execute(MessageT message, BotT bot, object arg = null)
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
		public sealed override Task<bool> ExecuteAsync(MessageT message, BotT bot, object arg = null)
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

		protected void AttributedBeforeExecute(MessageT message, BotT bot, object arg = null)
		{
			BeforeExecute(message, bot, arg);
			BaseCommand.BeforeExecute(message, bot, arg);
		}
		protected void AttributedAfterExecute(MessageT message, BotT bot, object arg = null)
		{
			AfterExecute(message, bot, arg);
			BaseCommand.AfterExecute(message, bot, arg);
		}
	}
	public abstract class AttributedCommand<BotT> : AttributedCommand<BotT, IMessage>
		where BotT : class, IBot
	{
		protected AttributedCommand(Command<BotT> baseCommand) : base(baseCommand)
		{
		}
	}
}
