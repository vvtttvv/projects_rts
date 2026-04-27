using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Realizations;
using Moq;

namespace BlogApp.Tests.Services;

[TestFixture]
public class PostServiceTests
{
    [Test]
    public async Task CreateAsync_ValidPost_TrimAndPersist()
    {
        var userId = Guid.NewGuid();
        var toCreate = TestDataFactory.Post(title: "  Hello  ", description: "  Text  ", userId: userId);
        var persisted = TestDataFactory.Post(title: "Hello", description: "Text", userId: userId);

        var repository = new Mock<IPostRepository>();
        repository
            .Setup(x => x.ExistsByUserAndTitleAsync(userId, "Hello", null))
            .ReturnsAsync(false);
        repository.Setup(x => x.AddAsync(It.IsAny<Post>())).ReturnsAsync(persisted);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var service = new PostService(repository.Object, userRepository.Object);

        var result = await service.CreateAsync(toCreate);

        Assert.Multiple(() =>
        {
            Assert.That(toCreate.Title, Is.EqualTo("Hello"));
            Assert.That(toCreate.Description, Is.EqualTo("Text"));
            Assert.That(result, Is.EqualTo(persisted));
        });
    }

    [Test]
    public void CreateAsync_EmptyTitle_ThrowsValidationException()
    {
        var userRepository = new Mock<IUserRepository>();
        var repository = new Mock<IPostRepository>();
        var service = new PostService(repository.Object, userRepository.Object);

        var post = TestDataFactory.Post(title: "  ");

        Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(post));
    }

    [Test]
    public void CreateAsync_UnknownUser_ThrowsEntityNotFoundException()
    {
        var userId = Guid.NewGuid();
        var post = TestDataFactory.Post(userId: userId);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        var repository = new Mock<IPostRepository>();
        var service = new PostService(repository.Object, userRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.CreateAsync(post));
    }

    [Test]
    public void CreateAsync_DuplicateTitleForSameUser_ThrowsConflictException()
    {
        var userId = Guid.NewGuid();
        var post = TestDataFactory.Post(title: "  Draft  ", userId: userId);

        var repository = new Mock<IPostRepository>();
        repository
            .Setup(x => x.ExistsByUserAndTitleAsync(userId, "Draft", null))
            .ReturnsAsync(true);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var service = new PostService(repository.Object, userRepository.Object);

        Assert.ThrowsAsync<ConflictException>(() => service.CreateAsync(post));
    }

    [Test]
    public async Task CreateAsync_SameTitleDifferentUser_IsAllowed()
    {
        var userId = Guid.NewGuid();
        var post = TestDataFactory.Post(title: "Draft", userId: userId);

        var repository = new Mock<IPostRepository>();
        repository
            .Setup(x => x.ExistsByUserAndTitleAsync(userId, "Draft", null))
            .ReturnsAsync(false);
        repository.Setup(x => x.AddAsync(post)).ReturnsAsync(post);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var service = new PostService(repository.Object, userRepository.Object);

        var result = await service.CreateAsync(post);

        Assert.That(result, Is.EqualTo(post));
    }

    [Test]
    public void UpdateAsync_MissingCurrent_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<IPostRepository>();
        repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Post?)null);

        var userRepository = new Mock<IUserRepository>();
        var service = new PostService(repository.Object, userRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.NewGuid(), TestDataFactory.Post()));
    }

    [Test]
    public async Task UpdateAsync_ValidPost_UpdatesAndReturnsPersisted()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var current = TestDataFactory.Post(id: id, title: "Old", description: "Old d", userId: userId);
        var incoming = TestDataFactory.Post(title: "  New  ", description: "  New d  ", userId: userId);

        var repository = new Mock<IPostRepository>();
        repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(current);
        repository
            .Setup(x => x.ExistsByUserAndTitleAsync(userId, "New", id))
            .ReturnsAsync(false);
        repository.Setup(x => x.UpdateByIdAsync(id, current)).ReturnsAsync(current);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var service = new PostService(repository.Object, userRepository.Object);

        var result = await service.UpdateAsync(id, incoming);

        Assert.Multiple(() =>
        {
            Assert.That(current.Title, Is.EqualTo("New"));
            Assert.That(current.Description, Is.EqualTo("New d"));
            Assert.That(result, Is.EqualTo(current));
        });
    }

    [Test]
    public void DeleteAsync_MissingEntity_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<IPostRepository>();
        repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        var userRepository = new Mock<IUserRepository>();
        var service = new PostService(repository.Object, userRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.NewGuid()));
    }
}

