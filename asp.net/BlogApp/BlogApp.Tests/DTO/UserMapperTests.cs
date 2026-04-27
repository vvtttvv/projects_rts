using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Users;
using BlogApp.Domain.Enums;

namespace BlogApp.Tests.DTO;

[TestFixture]
public class UserMapperTests
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
}


