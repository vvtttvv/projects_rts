using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;

namespace Database;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseLayer(this IServiceCollection services)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		return services;
	}
}

// Give more attention to this file