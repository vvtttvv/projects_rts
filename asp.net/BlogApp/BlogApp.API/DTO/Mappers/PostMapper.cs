using BlogApp.API.DTO.Models.Posts;
using BlogApp.Domain.Entities;

namespace BlogApp.API.DTO.Mappers;

public static class PostMapper
{
	public static Post ToEntity(this CreatePostRequest request) => new()
	{
		Title = request.Title,
		Description = request.Description ?? string.Empty,
		UserId = request.UserId
	};

	public static Post ToEntity(this UpdatePostRequest request) => new()
	{
		Title = request.Title,
		Description = request.Description ?? string.Empty,
		UserId = request.UserId
	};

	public static Post ToEntity(this CreatePostForUserRequest request, Guid userId) => new()
	{
		Title = request.Title,
		Description = request.Description ?? string.Empty,
		UserId = userId
	};

	public static PostResponse ToResponse(this Post post) => new(
		post.Id,
		post.Title,
		post.Description,
		post.UserId,
		post.CreatedAt,
		post.UpdatedAt
	);
}
