using Microsoft.Extensions.DependencyInjection;

using Store.Services.Interfaces;
using Store.Services.Realizations;

namespace Store.Services;

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
