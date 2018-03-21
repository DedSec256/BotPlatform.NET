using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public delegate void Callback(IMessage message, ISingleBot singleBot, object arg = null);


	public delegate void Callback<in BotType>(IMessage message, BotType bot, object arg = null);
}