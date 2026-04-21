using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.PostgreSQL.Repositories;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddPostgreSqlDatabase(configuration);
		services.AddPostgreSqlRepositories();
		return services;
	}

	public static IServiceCollection AddPostgreSqlDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

		services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsAssembly("Store.Api")));
		return services;
	}

	public static IServiceCollection AddPostgreSqlRepositories(this IServiceCollection services)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		return services;
	}
}

