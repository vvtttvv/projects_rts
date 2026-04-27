using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Posts;

namespace BlogApp.Tests.DTO;

[TestFixture]
public class PostMapperTests
{
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
}

