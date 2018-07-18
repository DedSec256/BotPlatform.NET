namespace BotPlatfrom.Kernel.System.Commands
{
	public abstract class AttributedCommand<TBot, TMessage> : Command<TBot, TMessage> 
	{
		/// <summary>
		/// Декорируемая команда
		/// </summary>
		protected Command<TBot, TMessage> BaseCommand;
		protected AttributedCommand(Command<TBot, TMessage> baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}
	}
}