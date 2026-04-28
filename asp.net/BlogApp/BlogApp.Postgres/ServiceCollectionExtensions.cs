using BlogApp.Postgres.Repositories;
using BlogApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Postgres;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseLayer(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

		services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
		services.AddPostgreSqlRepositories();
		return services;
	}

	public static IServiceCollection AddPostgreSqlRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICommentRepository, CommentRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IPostRepository, PostRepository>();
		return services;
	}
}
