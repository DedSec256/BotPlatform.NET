using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotPlatform.Core.Users
{
	public interface IUserRepository<TUser>
	{
		Task<bool> AddAsync(TUser user);
		Task<bool> DeleteAsync(TUser user);
		Task<TUser> GetAsync(Func<TUser, bool> predicate);
		Task<IEnumerable<TUser>> GetAllAsync();
		Task<IEnumerable<TUser>> GetAllAsync(Func<TUser, bool> predicate);
	}
}