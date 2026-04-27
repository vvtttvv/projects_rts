using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgres.Repositories;

public class PostRepository(AppDbContext dbContext) : GenericRepository<Post>(dbContext), IPostRepository
{
	public Task<bool> ExistsByUserAndTitleAsync(Guid userId, string title, Guid? excludingId = null)
	{
		var normalizedTitle = title.Trim().ToLower();

		return DbContext.Posts
			.AsNoTracking()
			.AnyAsync(x =>
				x.UserId == userId &&
				x.Title.ToLower() == normalizedTitle &&
				(!excludingId.HasValue || x.Id != excludingId.Value));
	}
}
