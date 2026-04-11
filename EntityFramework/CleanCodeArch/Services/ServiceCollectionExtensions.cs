using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Realizations;

namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}
