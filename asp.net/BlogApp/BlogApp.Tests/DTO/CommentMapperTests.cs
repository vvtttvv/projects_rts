using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Comments;

namespace BlogApp.Tests.DTO;

[TestFixture]
public class CommentMapperTests
{
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

