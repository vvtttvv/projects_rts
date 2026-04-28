using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Users;
using BlogApp.API.Extensions;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.API.Endpoints;

public static class UsersEndpoints
{
	public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
	{
		var usersGroup = group.MapGroup("/users").WithTags("Users");

		usersGroup.MapGet("/", GetAllUsersAsync)
			.Produces<PagedResult<UserResponse>>();

		usersGroup.MapGet("/{id:guid}", GetUserByIdAsync)
			.Produces<UserResponse>()
			.Produces(StatusCodes.Status404NotFound);

		usersGroup.MapPost("/", CreateUserAsync)
			.Produces<UserResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status409Conflict);

		usersGroup.MapPut("/{id:guid}", UpdateUserAsync)
			.Produces<UserResponse>()
			.ProducesValidationProblem()
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status409Conflict);

		usersGroup.MapDelete("/{id:guid}", DeleteUserAsync)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound);

		usersGroup.MapPost("/with-post", CreateUserWithPostAsync)
			.Produces<UserWithPostResponse>(StatusCodes.Status201Created)
			.ProducesValidationProblem();

		return group;
	}

	private static async Task<IResult> GetAllUsersAsync(IUserService service, int page = 1, int pageSize = 10)
	{
		var users = await service.GetAllAsync(page, pageSize);
		return Results.Ok(new PagedResult<UserResponse>(
			users.Items.Select(x => x.ToResponse()).ToArray(),
			users.Page,
			users.PageSize,
			users.TotalCount));
	}

	private static async Task<IResult> GetUserByIdAsync(Guid id, IUserService service)
	{
		var user = await service.GetByIdAsync(id);
		return user is null ? Results.NotFound() : Results.Ok(user.ToResponse());
	}

	private static async Task<IResult> CreateUserAsync(CreateUserRequest request, IUserService service)
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
	}

	private static async Task<IResult> UpdateUserAsync(Guid id, UpdateUserRequest request, IUserService service)
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

	private static async Task<IResult> DeleteUserAsync(Guid id, IUserService service)
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

	private static async Task<IResult> CreateUserWithPostAsync(CreateUserWithPostRequest request, IGuestPost guestPost)
	{
		var validationErrors = request.ValidateRequest();

		if (validationErrors is not null)
		{
			return Results.ValidationProblem(validationErrors);
		}

		var user = request.User.ToEntity();
		var post = request.Post.ToEntity(user.Id);

		await guestPost.AddUserAndPostAsync(user, post);
		var response = new UserWithPostResponse(user.ToResponse(), post.ToResponse());
		return Results.Created($"/api/users/{response.User.Id}", response);
	}
}