namespace BotPlatfrom.Kernel.System
{
	public class CommandCenterPoint<TBot, TMessage>
	{
		private readonly CommandCenter _instance = CommandCenter.Instance;

		public virtual bool TryAdd(string commandName, Callback<TBot, TMessage> callback)
		{
			return _instance.TryAdd(commandName, callback);
		}
	}
}
