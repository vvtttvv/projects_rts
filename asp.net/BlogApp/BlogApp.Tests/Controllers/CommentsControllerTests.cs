using BlogApp.API.Controllers;
using BlogApp.API.DTO.Models.Comments;
using BlogApp.Services.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Tests.Controllers;

[TestFixture]
public class CommentsControllerTests
{
    [Test]
    public async Task GetById_NotFound_Returns404()
    {
        var service = new Mock<ICommentService>();
        service.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((BlogApp.Domain.Entities.Comment?)null);

        var controller = new CommentsController(service.Object);

        var result = await controller.GetById(Guid.NewGuid());

        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_Returns201CreatedAtAction()
    {
        var created = TestDataFactory.Comment();

        var service = new Mock<ICommentService>();
        service.Setup(x => x.CreateAsync(It.IsAny<BlogApp.Domain.Entities.Comment>())).ReturnsAsync(created);

        var controller = new CommentsController(service.Object);

        var request = new CreateCommentRequest("text", created.UserId, created.PostId, null);
        var result = await controller.Create(request);

        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }
}

