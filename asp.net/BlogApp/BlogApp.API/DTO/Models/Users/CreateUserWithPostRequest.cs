using BlogApp.API.DTO.Models.Posts;

namespace BlogApp.API.DTO.Models.Users;

public record CreateUserWithPostRequest(
	CreateUserRequest User,
	CreatePostForUserRequest Post
);

