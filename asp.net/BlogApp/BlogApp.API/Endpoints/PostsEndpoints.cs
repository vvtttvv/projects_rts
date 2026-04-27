using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Posts;
using BlogApp.API.Extensions;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class PostsEndpoints
{
	public static IEndpointRouteBuilder MapPostsEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/api/posts").WithTags("Posts");

		group.MapGet("/", async (IPostService service) =>
			{
				var posts = await service.GetAllAsync();
				return Results.Ok(posts.Select(x => x.ToResponse()).ToArray());
			})
			.Produces<IReadOnlyCollection<PostResponse>>(StatusCodes.Status200OK);

		group.MapGet("/{id:guid}", async (Guid id, IPostService service) =>
			{
				var post = await service.GetByIdAsync(id);
				return post is null ? Results.NotFound() : Results.Ok(post.ToResponse());
			})
			.Produces<PostResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

		group.MapPost("/", async (CreatePostRequest request, IPostService service) =>
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
					return Results.Created($"/api/posts/{response.Id}", response);
				}
				catch (ServiceException exception)
				{
					return exception.ToHttpResult();
				}
			})
			.Produces<PostResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		group.MapPut("/{id:guid}", async (Guid id, UpdatePostRequest request, IPostService service) =>
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
			.Produces<PostResponse>(StatusCodes.Status200OK)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		group.MapDelete("/{id:guid}", async (Guid id, IPostService service) =>
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

