using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotPlatform.Core.Users;

namespace TelegramBot.Communication
{
	public class UserRepository : IUserRepository<User>
	{
		public Task<bool> AddAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetAsync(Func<User, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<User>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<User>> GetAllAsync(Func<User, bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}