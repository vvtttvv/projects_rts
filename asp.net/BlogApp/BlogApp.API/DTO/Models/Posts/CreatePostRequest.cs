using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Posts;

public record CreatePostRequest(
	[Required, MaxLength(200)] string Title,
	[MaxLength(2000)] string? Description,
	[Required] Guid UserId
);
