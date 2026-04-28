using BlogApp.Services.Interfaces;
using BlogApp.Services.Realizations;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGuestPost, GuestPost>();
        return services;
    }
}
