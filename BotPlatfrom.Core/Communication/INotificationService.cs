using System;
using System.Threading.Tasks;

namespace BotPlatform.Core.Communication
{
	public interface INotificationService<in TBot, out TUser>
	{
		Task SendAsync(TBot bot, Func<TUser, bool> predicate, Func<TUser, string> messageBuilder);
		Task SendAsyncWith<TMessage>(TBot bot, Func<TUser, bool> predicate, Func<TUser, TMessage> messageBuilder);
	}
}