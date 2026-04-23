using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Comments;
using BlogApp.API.DTO.Models.Posts;
using BlogApp.API.DTO.Models.Users;
using BlogApp.Domain.Enums;

namespace BlogApp.Tests.DTO;

[TestFixture]
public class MappersTests
{
    [Test]
    public void UserMapper_CreateRequest_ToEntity()
    {
        var request = new CreateUserRequest("john", 33, "John Doe", UserType.Admin);

        var entity = request.ToEntity();

        Assert.Multiple(() =>
        {
            Assert.That(entity.UserName, Is.EqualTo("john"));
            Assert.That(entity.Age, Is.EqualTo(33));
            Assert.That(entity.FullName, Is.EqualTo("John Doe"));
            Assert.That(entity.Role, Is.EqualTo(UserType.Admin));
        });
    }

    [Test]
    public void UserMapper_Entity_ToResponse()
    {
        var user = TestDataFactory.User();

        var response = user.ToResponse();

        Assert.That(response.Id, Is.EqualTo(user.Id));
    }

    [Test]
    public void PostMapper_CreateRequest_NullDescription_ToEmptyString()
    {
        var userId = Guid.NewGuid();
        var request = new CreatePostRequest("Title", null, userId);

        var entity = request.ToEntity();

        Assert.Multiple(() =>
        {
            Assert.That(entity.Title, Is.EqualTo("Title"));
            Assert.That(entity.Description, Is.EqualTo(string.Empty));
            Assert.That(entity.UserId, Is.EqualTo(userId));
        });
    }

    [Test]
    public void PostMapper_Entity_ToResponse()
    {
        var post = TestDataFactory.Post();

        var response = post.ToResponse();

        Assert.That(response.Title, Is.EqualTo(post.Title));
    }

    [Test]
    public void CommentMapper_UpdateRequest_ToEntity()
    {
        var userId = Guid.NewGuid();
        var postId = Guid.NewGuid();
        var parentId = Guid.NewGuid();
        var request = new UpdateCommentRequest("hello", userId, postId, parentId);

        var entity = request.ToEntity();

        Assert.Multiple(() =>
        {
            Assert.That(entity.Description, Is.EqualTo("hello"));
            Assert.That(entity.UserId, Is.EqualTo(userId));
            Assert.That(entity.PostId, Is.EqualTo(postId));
            Assert.That(entity.ParentId, Is.EqualTo(parentId));
        });
    }

    [Test]
    public void CommentMapper_Entity_ToResponse()
    {
        var comment = TestDataFactory.Comment();

        var response = comment.ToResponse();

        Assert.That(response.Description, Is.EqualTo(comment.Description));
    }
}

