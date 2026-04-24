using BlogApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Users;

public record UpdateUserRequest(
	[Required, MaxLength(100)] string UserName,
	[Range(1, 150)] int Age,
	[Required, MaxLength(200)] string FullName,
	UserType Role = UserType.Default
);
