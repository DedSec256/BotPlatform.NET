﻿using System;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public abstract class Command
	{
		public Type BotType { get; protected set; } = typeof(ISingleBot);
		public Type MessageType { get; protected set; } = typeof(IMessage);
		protected abstract void BeforeExecute(IMessage message, ISingleBot singleBot, object arg = null);
		public abstract Task<bool> ExecuteAsync(IMessage message, ISingleBot singleBot, object arg = null);
		public abstract bool Execute(IMessage message, ISingleBot singleBot, object arg = null);
		protected abstract void AfterExecute(IMessage message, ISingleBot singleBot, object arg = null);

	}
	//TODO: CHANGING
	public class Command<BotT> : Command where BotT : class, ISingleBot
	{
		public readonly Callback<BotT> Callback;
		public sealed override Task<bool> ExecuteAsync(IMessage message, ISingleBot singleBot, object arg = null)
		{
			var botT = singleBot as BotT;
			if(botT != null) return ExecuteAsync(message, singleBot as BotT, arg);

			throw new ArgumentException($"Cannot convert type '{singleBot.GetType()}' to '{typeof(BotT)}'");
		}
		public sealed override bool Execute(IMessage message, ISingleBot singleBot, object arg = null)
		{
			return Execute(message, singleBot as BotT, arg);
		}
		protected sealed override void BeforeExecute(IMessage message, ISingleBot singleBot, object arg = null)
		{	
			BeforeExecute(message, singleBot as BotT , arg);
		}
		protected sealed override void AfterExecute(IMessage message, ISingleBot singleBot, object arg = null)
		{
			AfterExecute(message, singleBot as BotT, arg);
		}

		public virtual void BeforeExecute(IMessage message, BotT bot, object arg = null)
		{

		}
		public virtual void AfterExecute(IMessage message, BotT bot, object arg = null)
		{

		}

		public Task<bool> ExecuteAsync(IMessage message, BotT bot, object arg = null)
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
				catch (Exception ex)
				{
					return false;
				}
			});
		}

		public bool Execute(IMessage message, BotT bot, object arg = null)
		{
			try
			{
				BeforeExecute(message, bot, arg);
				Callback(message, bot, arg);
				AfterExecute(message, bot, arg);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Command(Callback<BotT> callback)
		{
			Callback = callback;
			BotType = typeof(BotT);
		}
	}

	public abstract class AttributedCommand<BotT> : Command<BotT> where BotT : class, ISingleBot
	{
		protected Command<BotT> BaseCommand;
		protected AttributedCommand(Command<BotT> baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}
	}
}
