using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Realizations;
using Moq;

namespace BlogApp.Tests.Services;

[TestFixture]
public class UserServiceTests
{
    [Test]
    public async Task GetAllAsync_ReturnsRepositoryResult()
    {
        var expected = new[] { TestDataFactory.User() };
        var repository = new Mock<IUserRepository>();
        repository
            .Setup(x => x.GetAllAsync(0, 0, It.IsAny<List<System.Linq.Expressions.Expression<Func<User, object>>>>(), true))
            .ReturnsAsync(expected);

        var service = new UserService(repository.Object);

        var result = await service.GetAllAsync();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task CreateAsync_ValidUser_TrimAndPersist()
    {
        var toCreate = TestDataFactory.User(userName: "  Alice  ", fullName: "  Alice Johnson  ");
        var persisted = TestDataFactory.User(userName: "Alice", fullName: "Alice Johnson");

        var repository = new Mock<IUserRepository>();
        repository
            .Setup(x => x.GetAllAsync(0, 0, It.IsAny<List<System.Linq.Expressions.Expression<Func<User, object>>>>(), true))
            .ReturnsAsync(Array.Empty<User>());
        repository
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(persisted);

        var service = new UserService(repository.Object);

        var result = await service.CreateAsync(toCreate);

        Assert.Multiple(() =>
        {
            Assert.That(toCreate.UserName, Is.EqualTo("Alice"));
            Assert.That(toCreate.FullName, Is.EqualTo("Alice Johnson"));
            Assert.That(result, Is.EqualTo(persisted));
        });
    }

    [Test]
    public void CreateAsync_EmptyUserName_ThrowsValidationException()
    {
        var repository = new Mock<IUserRepository>();
        var service = new UserService(repository.Object);

        var user = TestDataFactory.User(userName: "   ");

        Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(user));
    }

    [Test]
    public void CreateAsync_DuplicateUserName_ThrowsConflictException()
    {
        var existing = TestDataFactory.User(userName: "john");
        var user = TestDataFactory.User(userName: " JOHN ");

        var repository = new Mock<IUserRepository>();
        repository
            .Setup(x => x.GetAllAsync(0, 0, It.IsAny<List<System.Linq.Expressions.Expression<Func<User, object>>>>(), true))
            .ReturnsAsync(new[] { existing });

        var service = new UserService(repository.Object);

        Assert.ThrowsAsync<ConflictException>(() => service.CreateAsync(user));
    }

    [Test]
    public void UpdateAsync_MissingCurrent_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

        var service = new UserService(repository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.NewGuid(), TestDataFactory.User()));
    }

    [Test]
    public async Task UpdateAsync_ValidUser_UpdatesAndReturnsPersisted()
    {
        var id = Guid.NewGuid();
        var current = TestDataFactory.User(id: id, userName: "old", fullName: "Old Name", age: 20);
        var incoming = TestDataFactory.User(userName: "  new-login  ", fullName: "  New Name  ", age: 30);

        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(current);
        repository
            .Setup(x => x.GetAllAsync(0, 0, It.IsAny<List<System.Linq.Expressions.Expression<Func<User, object>>>>(), true))
            .ReturnsAsync(Array.Empty<User>());
        repository.Setup(x => x.UpdateByIdAsync(id, current)).ReturnsAsync(current);

        var service = new UserService(repository.Object);

        var result = await service.UpdateAsync(id, incoming);

        Assert.Multiple(() =>
        {
            Assert.That(current.UserName, Is.EqualTo("new-login"));
            Assert.That(current.FullName, Is.EqualTo("New Name"));
            Assert.That(current.Age, Is.EqualTo(30));
            Assert.That(result, Is.EqualTo(current));
        });
    }

    [Test]
    public void DeleteAsync_MissingEntity_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        var service = new UserService(repository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.NewGuid()));
    }

    [Test]
    public async Task DeleteAsync_ExistingEntity_Completes()
    {
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        var service = new UserService(repository.Object);

        Assert.DoesNotThrowAsync(() => service.DeleteAsync(Guid.NewGuid()));
    }
}

