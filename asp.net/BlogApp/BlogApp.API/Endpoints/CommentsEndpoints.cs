using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Comments;
using BlogApp.API.Extensions;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class CommentsEndpoints
{
	public static IEndpointRouteBuilder MapCommentsEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/api/comments").WithTags("Comments");

		group.MapGet("/", async (ICommentService service) =>
			{
				var comments = await service.GetAllAsync();
				return Results.Ok(comments.Select(x => x.ToResponse()).ToArray());
			})
			.Produces<IReadOnlyCollection<CommentResponse>>(StatusCodes.Status200OK);

		group.MapGet("/{id:guid}", async (Guid id, ICommentService service) =>
			{
				var comment = await service.GetByIdAsync(id);
				return comment is null ? Results.NotFound() : Results.Ok(comment.ToResponse());
			})
			.Produces<CommentResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

		group.MapPost("/", async (CreateCommentRequest request, ICommentService service) =>
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
			})
			.Produces<CommentResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);

		group.MapPut("/{id:guid}", async (Guid id, UpdateCommentRequest request, ICommentService service) =>
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
			.Produces<CommentResponse>(StatusCodes.Status200OK)
			.ProducesValidationProblem(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);

		group.MapDelete("/{id:guid}", async (Guid id, ICommentService service) =>
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

