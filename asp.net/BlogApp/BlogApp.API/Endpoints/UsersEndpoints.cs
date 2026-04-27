using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Users;
using BlogApp.API.Extensions;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class UsersEndpoints
{
	public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/api/users").WithTags("Users");

		group.MapGet("/", async (IUserService service) =>
			{
				var users = await service.GetAllAsync();
				return Results.Ok(users.Select(x => x.ToResponse()).ToArray());
			})
			.Produces<IReadOnlyCollection<UserResponse>>(StatusCodes.Status200OK);

		group.MapGet("/{id:guid}", async (Guid id, IUserService service) =>
			{
				var user = await service.GetByIdAsync(id);
				return user is null ? Results.NotFound() : Results.Ok(user.ToResponse());
			})
			.Produces<UserResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

		group.MapPost("/", async (CreateUserRequest request, IUserService service) =>
			{
				var validationErrors = request.ValidateRequest();
				if (validationErrors is not null)
				{
					return Results.ValidationProblem(validationErrors);
				}

				try
				{
					var created = await service.CreateAsync(request.ToEntity());
					var response = created.ToResponse();
					return Results.Created($"/api/users/{response.Id}", response);
				}
				catch (ServiceException exception)
				{
					return exception.ToHttpResult();
				}
			})
			.Produces<UserResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status409Conflict);

		group.MapPut("/{id:guid}", async (Guid id, UpdateUserRequest request, IUserService service) =>
			{
				var validationErrors = request.ValidateRequest();
				if (validationErrors is not null)
				{
					return Results.ValidationProblem(validationErrors);
				}

				try
				{
					var updated = await service.UpdateAsync(id, request.ToEntity());
					return Results.Ok(updated.ToResponse());
				}
				catch (ServiceException exception)
				{
					return exception.ToHttpResult();
				}
			})
			.Produces<UserResponse>(StatusCodes.Status200OK)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		group.MapDelete("/{id:guid}", async (Guid id, IUserService service) =>
			{
				try
				{
					await service.DeleteAsync(id);
					return Results.NoContent();
				}
				catch (EntityNotFoundException)
				{
					return Results.NotFound();
				}
			})
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound);

		return endpoints;
	}
}

