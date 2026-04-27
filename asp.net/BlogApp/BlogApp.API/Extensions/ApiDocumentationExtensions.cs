using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.API.Extensions;

public static class ApiDocumentationExtensions
{
	public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		return services;
	}

	public static WebApplication UseApiDocumentation(this WebApplication app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();
		return app;
	}
}

