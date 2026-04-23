using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Realizations;
using Moq;

namespace BlogApp.Tests.Services;

[TestFixture]
public class CommentServiceTests
{
    [Test]
    public async Task CreateAsync_ValidComment_TrimAndPersist()
    {
        var userId = Guid.NewGuid();
        var postId = Guid.NewGuid();
        var toCreate = TestDataFactory.Comment(description: "  Hi there  ", userId: userId, postId: postId);

        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.AddAsync(toCreate)).ReturnsAsync(toCreate);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(TestDataFactory.Post(id: postId, userId: userId));

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        var result = await service.CreateAsync(toCreate);

        Assert.Multiple(() =>
        {
            Assert.That(toCreate.Description, Is.EqualTo("Hi there"));
            Assert.That(result, Is.EqualTo(toCreate));
        });
    }

    [Test]
    public void CreateAsync_EmptyDescription_ThrowsValidationException()
    {
        var service = new CommentService(
            new Mock<ICommentRepository>().Object,
            new Mock<IUserRepository>().Object,
            new Mock<IPostRepository>().Object);

        Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(TestDataFactory.Comment(description: "  ")));
    }

    [Test]
    public void CreateAsync_UnknownUser_ThrowsEntityNotFoundException()
    {
        var comment = TestDataFactory.Comment();

        var repository = new Mock<ICommentRepository>();
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(comment.UserId)).ReturnsAsync((User?)null);

        var postRepository = new Mock<IPostRepository>();
        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.CreateAsync(comment));
    }

    [Test]
    public void CreateAsync_UnknownPost_ThrowsEntityNotFoundException()
    {
        var comment = TestDataFactory.Comment();

        var repository = new Mock<ICommentRepository>();

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(comment.UserId)).ReturnsAsync(TestDataFactory.User(id: comment.UserId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(comment.PostId)).ReturnsAsync((Post?)null);

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.CreateAsync(comment));
    }

    [Test]
    public void CreateAsync_MissingParent_ThrowsEntityNotFoundException()
    {
        var parentId = Guid.NewGuid();
        var comment = TestDataFactory.Comment(parentId: parentId);

        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.GetByIdAsync(parentId)).ReturnsAsync((Comment?)null);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(comment.UserId)).ReturnsAsync(TestDataFactory.User(id: comment.UserId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(comment.PostId)).ReturnsAsync(TestDataFactory.Post(id: comment.PostId, userId: comment.UserId));

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.CreateAsync(comment));
    }

    [Test]
    public void CreateAsync_ParentFromAnotherPost_ThrowsValidationException()
    {
        var parentId = Guid.NewGuid();
        var comment = TestDataFactory.Comment(parentId: parentId);
        var parent = TestDataFactory.Comment(id: parentId, postId: Guid.NewGuid());

        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.GetByIdAsync(parentId)).ReturnsAsync(parent);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(comment.UserId)).ReturnsAsync(TestDataFactory.User(id: comment.UserId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(comment.PostId)).ReturnsAsync(TestDataFactory.Post(id: comment.PostId, userId: comment.UserId));

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(comment));
    }

    [Test]
    public void UpdateAsync_ParentIsSelf_ThrowsValidationException()
    {
        var id = Guid.NewGuid();
        var current = TestDataFactory.Comment(id: id);
        var incoming = TestDataFactory.Comment(parentId: id, userId: current.UserId, postId: current.PostId);

        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(current);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(current.UserId)).ReturnsAsync(TestDataFactory.User(id: current.UserId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(current.PostId)).ReturnsAsync(TestDataFactory.Post(id: current.PostId, userId: current.UserId));

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        Assert.ThrowsAsync<ValidationException>(() => service.UpdateAsync(id, incoming));
    }

    [Test]
    public void UpdateAsync_MissingCurrent_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Comment?)null);

        var service = new CommentService(
            repository.Object,
            new Mock<IUserRepository>().Object,
            new Mock<IPostRepository>().Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.NewGuid(), TestDataFactory.Comment()));
    }

    [Test]
    public async Task UpdateAsync_ValidComment_UpdatesAndReturnsPersisted()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var postId = Guid.NewGuid();

        var current = TestDataFactory.Comment(id: id, description: "old", userId: userId, postId: postId);
        var incoming = TestDataFactory.Comment(description: "  new text  ", userId: userId, postId: postId);

        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(current);
        repository.Setup(x => x.UpdateByIdAsync(id, current)).ReturnsAsync(current);

        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(TestDataFactory.User(id: userId));

        var postRepository = new Mock<IPostRepository>();
        postRepository.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(TestDataFactory.Post(id: postId, userId: userId));

        var service = new CommentService(repository.Object, userRepository.Object, postRepository.Object);

        var result = await service.UpdateAsync(id, incoming);

        Assert.Multiple(() =>
        {
            Assert.That(current.Description, Is.EqualTo("new text"));
            Assert.That(current.UserId, Is.EqualTo(userId));
            Assert.That(current.PostId, Is.EqualTo(postId));
            Assert.That(result, Is.EqualTo(current));
        });
    }

    [Test]
    public void DeleteAsync_MissingEntity_ThrowsEntityNotFoundException()
    {
        var repository = new Mock<ICommentRepository>();
        repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        var service = new CommentService(
            repository.Object,
            new Mock<IUserRepository>().Object,
            new Mock<IPostRepository>().Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.NewGuid()));
    }
}

