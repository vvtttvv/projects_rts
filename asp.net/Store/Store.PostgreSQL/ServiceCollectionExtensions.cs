using Store.PostgreSQL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL;

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
