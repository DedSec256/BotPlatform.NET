namespace BotPlatfrom.Kernel.System
{
	public abstract class CommandsModule
	{
		public abstract void Initialize();
	}
	public abstract class CommandsModule<TBot, TMessage> : CommandsModule
	{
		protected readonly CommandCenterPoint<TBot, TMessage> CommandCenter = new CommandCenterPoint<TBot, TMessage>();
	}
}
