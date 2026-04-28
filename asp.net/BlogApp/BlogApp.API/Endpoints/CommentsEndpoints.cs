using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Comments;
using BlogApp.API.Extensions;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class CommentsEndpoints
{
	public static RouteGroupBuilder MapCommentsEndpoints(this RouteGroupBuilder group)
	{
		var commentsGroup = group.MapGroup("/comments").WithTags("Comments");

		commentsGroup.MapGet("/", GetAllCommentsAsync)
			.Produces<PagedResult<CommentResponse>>();

		commentsGroup.MapGet("/{id:guid}", GetCommentByIdAsync)
			.Produces<CommentResponse>()
			.Produces(StatusCodes.Status404NotFound);

		commentsGroup.MapPost("/", CreateCommentAsync)
			.Produces<CommentResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status404NotFound);

		commentsGroup.MapPut("/{id:guid}", UpdateCommentAsync)
			.Produces<CommentResponse>()
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status404NotFound);

		commentsGroup.MapDelete("/{id:guid}", DeleteCommentAsync)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound);

		return group;
	}

	private static async Task<IResult> GetAllCommentsAsync(ICommentService service, int page = 1, int pageSize = 10)
	{
		var comments = await service.GetAllAsync(page, pageSize);
		return Results.Ok(new PagedResult<CommentResponse>(
			comments.Items.Select(x => x.ToResponse()).ToArray(),
			comments.Page,
			comments.PageSize,
			comments.TotalCount));
	}

	private static async Task<IResult> GetCommentByIdAsync(Guid id, ICommentService service)
	{
		var comment = await service.GetByIdAsync(id);
		return comment is null ? Results.NotFound() : Results.Ok(comment.ToResponse());
	}

	private static async Task<IResult> CreateCommentAsync(CreateCommentRequest request, ICommentService service)
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
			return Results.Created($"/api/comments/{response.Id}", response);
		}
		catch (ServiceException exception)
		{
			return exception.ToHttpResult();
		}
	}

	private static async Task<IResult> UpdateCommentAsync(Guid id, UpdateCommentRequest request, ICommentService service)
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

	private static async Task<IResult> DeleteCommentAsync(Guid id, ICommentService service)
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

