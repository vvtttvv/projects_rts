using BlogApp.Domain.Entities;
using BlogApp.Domain.Enums;

namespace BlogApp.Tests;

internal static class TestDataFactory
{
    public static User User(
        Guid? id = null,
        string userName = "john",
        int age = 25,
        string fullName = "John Doe",
        UserType role = UserType.Default)
    {
        return new User
        {
            Id = id ?? Guid.NewGuid(),
            UserName = userName,
            Age = age,
            FullName = fullName,
            Role = role
        };
    }

    public static Post Post(
        Guid? id = null,
        string title = "post-title",
        string description = "post-description",
        Guid? userId = null)
    {
        return new Post
        {
            Id = id ?? Guid.NewGuid(),
            Title = title,
            Description = description,
            UserId = userId ?? Guid.NewGuid()
        };
    }

    public static Comment Comment(
        Guid? id = null,
        string description = "comment-text",
        Guid? userId = null,
        Guid? postId = null,
        Guid? parentId = null)
    {
        return new Comment
        {
            Id = id ?? Guid.NewGuid(),
            Description = description,
            UserId = userId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            ParentId = parentId
        };
    }
}

