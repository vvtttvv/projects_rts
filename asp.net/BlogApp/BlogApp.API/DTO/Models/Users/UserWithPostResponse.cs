using BlogApp.API.DTO.Models.Posts;

namespace BlogApp.API.DTO.Models.Users;

public record UserWithPostResponse(
	UserResponse User,
	PostResponse Post
);

