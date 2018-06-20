namespace BotPlatfrom.Kernel.System
{
	public delegate void Callback<in TBot, in TMessage>(TBot bot, TMessage message, object arg = null);
}