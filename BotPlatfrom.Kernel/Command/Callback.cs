using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public delegate void Callback<in TBot, in TMessage>(TMessage message, TBot bot, object arg = null);
}