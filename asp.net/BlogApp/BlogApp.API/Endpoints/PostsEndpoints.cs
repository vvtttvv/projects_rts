using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Posts;
using BlogApp.API.Extensions;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class PostsEndpoints
{
	public static RouteGroupBuilder MapPostsEndpoints(this RouteGroupBuilder group)
	{
		var postsGroup = group.MapGroup("/posts").WithTags("Posts");

		postsGroup.MapGet("/", GetAllPostsAsync)
			.Produces<PagedResult<PostResponse>>();

		postsGroup.MapGet("/{id:guid}", GetPostByIdAsync)
			.Produces<PostResponse>()
			.Produces(StatusCodes.Status404NotFound);

		postsGroup.MapPost("/", CreatePostAsync)
			.Produces<PostResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		postsGroup.MapPut("/{id:guid}", UpdatePostAsync)
			.Produces<PostResponse>()
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		postsGroup.MapDelete("/{id:guid}", DeletePostAsync)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound);

		return group;
	}

	private static async Task<IResult> GetAllPostsAsync(IPostService service, int page = 1, int pageSize = 10)
	{
		var posts = await service.GetAllAsync(page, pageSize);
		return Results.Ok(new PagedResult<PostResponse>(
			posts.Items.Select(x => x.ToResponse()).ToArray(),
			posts.Page,
			posts.PageSize,
			posts.TotalCount));
	}

	private static async Task<IResult> GetPostByIdAsync(Guid id, IPostService service)
	{
		var post = await service.GetByIdAsync(id);
		return post is null ? Results.NotFound() : Results.Ok(post.ToResponse());
	}

	private static async Task<IResult> CreatePostAsync(CreatePostRequest request, IPostService service)
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
	}

	private static async Task<IResult> UpdatePostAsync(Guid id, UpdatePostRequest request, IPostService service)
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
	}

	private static async Task<IResult> DeletePostAsync(Guid id, IPostService service)
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
	}
}

