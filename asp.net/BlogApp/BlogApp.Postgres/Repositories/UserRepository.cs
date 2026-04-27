using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgres.Repositories;

public class UserRepository(AppDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
	public Task<bool> ExistsByUserNameAsync(string userName, Guid? excludingId = null)
	{
		var normalizedUserName = userName.Trim().ToLower();

		return DbContext.Users
			.AsNoTracking()
			.AnyAsync(x =>
				x.UserName.ToLower() == normalizedUserName &&
				(!excludingId.HasValue || x.Id != excludingId.Value));
	}
}
