using System;
using System.Threading.Tasks;
using BotPlatform.Core.Communication;
using BotPlatform.Core.Tools;
using BotPlatform.Core.Users;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using MessageType = BotPlatform.Core.Tools.MessageType;

namespace TelegramBot.Communication
{
	public class NotificationService : INotificationService<TelegramBotClient, User>
	{
		private readonly IUserRepository<User> _userRepository;

		public NotificationService(IUserRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public Task SendAsync(TelegramBotClient bot, Func<User, bool> predicate, Func<User, string> messageBuilder)
		{
			return Task.Run(async () =>
			{
				var userList = await _userRepository.GetAllAsync(predicate);
				foreach (var user in userList)
				{
					var message = messageBuilder(user);
					if (message != null)
					{
						await bot.SendTextMessageAsync(user.Id, message, ParseMode.Markdown);
						BotConsole.Write($"Уведомление было отправлено пользователю с id {user.Id}", MessageType.System);
					}
				}
			});
		}

		public Task SendAsyncWith<TMessage>(TelegramBotClient bot, Func<User, bool> predicate, Func<User, TMessage> messageBuilder)
		{
			throw new NotImplementedException();
		}
	}
}