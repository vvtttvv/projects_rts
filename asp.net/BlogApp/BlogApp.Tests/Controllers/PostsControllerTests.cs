using BlogApp.API.Controllers;
using BlogApp.Services.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Tests.Controllers;

[TestFixture]
public class PostsControllerTests
{
    [Test]
    public async Task GetById_NotFound_Returns404()
    {
        var service = new Mock<IPostService>();
        service.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((BlogApp.Domain.Entities.Post?)null);

        var controller = new PostsController(service.Object);

        var result = await controller.GetById(Guid.NewGuid());

        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Delete_Returns204NoContent()
    {
        var service = new Mock<IPostService>();
        service.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var controller = new PostsController(service.Object);

        var result = await controller.Delete(Guid.NewGuid());

        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}

