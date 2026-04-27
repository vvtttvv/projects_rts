using BlogApp.API.Controllers;
using BlogApp.API.DTO.Models.Users;
using BlogApp.Services.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Tests.Controllers;

[TestFixture]
public class UsersControllerTests
{
    [Test]
    public async Task GetById_NotFound_Returns404()
    {
        var service = new Mock<IUserService>();
        service.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((BlogApp.Domain.Entities.User?)null);

        var controller = new UsersController(service.Object);

        var result = await controller.GetById(Guid.NewGuid());

        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_Returns201CreatedAtAction()
    {
        var created = TestDataFactory.User();

        var service = new Mock<IUserService>();
        service.Setup(x => x.CreateAsync(It.IsAny<BlogApp.Domain.Entities.User>())).ReturnsAsync(created);

        var controller = new UsersController(service.Object);

        var result = await controller.Create(new CreateUserRequest("john", 20, "John Doe"));


        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }
}


