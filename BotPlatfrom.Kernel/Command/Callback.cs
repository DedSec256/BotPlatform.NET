using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public delegate void Callback(IMessage message, IBot bot, object arg = null);
	public delegate void Callback<in BotType, in MessageType>(MessageType message, BotType bot, object arg = null);
}