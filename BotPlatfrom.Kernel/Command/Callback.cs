using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public delegate void Callback(Message message, ISingleBot singleBot, object arg = null);


	public delegate void Callback<in BotType>(Message message, BotType bot, object arg = null);
}