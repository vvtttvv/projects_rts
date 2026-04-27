using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgres.Seed;

public static class DbSeeder
{
	public static async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		var hasUsers = await dbContext.Users.AnyAsync(cancellationToken);
		var hasPosts = await dbContext.Posts.AnyAsync(cancellationToken);
		var hasComments = await dbContext.Comments.AnyAsync(cancellationToken);

		if (hasUsers && hasPosts && hasComments)
		{
			return;
		}

		var users = await UserSeeder.SeedAsync(dbContext, cancellationToken);
		var posts = await PostSeeder.SeedAsync(dbContext, users, cancellationToken);
		await CommentSeeder.SeedAsync(dbContext, users, posts, cancellationToken);
	}
}
