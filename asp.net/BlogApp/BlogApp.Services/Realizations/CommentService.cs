using System.Linq.Expressions;
using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services.Realizations;

public class CommentService(
	ICommentRepository repository,
	IUserRepository userRepository,
	IPostRepository postRepository) : ICommentService
{
	public Task<IReadOnlyCollection<Comment>> GetAllAsync() =>
		repository.GetAllAsync();

	public Task<Comment?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Comment> CreateAsync(Comment comment)
	{
		await ValidateAsync(comment);
		comment.Description = comment.Description.Trim();
		return await repository.AddAsync(comment);
	}

	public async Task<Comment> UpdateAsync(Guid id, Comment comment)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"Comment with id {id} was not found.");

		await ValidateAsync(comment, id);

		current.Description = comment.Description.Trim();
		current.UserId = comment.UserId;
		current.PostId = comment.PostId;
		current.ParentId = comment.ParentId;

		return await repository.UpdateByIdAsync(id, current)
			?? throw new EntityNotFoundException($"Comment with id {id} was not found.");
	}

	public async Task DeleteAsync(Guid id)
	{
		var deleted = await repository.DeleteAsync(id);
		if (!deleted)
		{
			throw new EntityNotFoundException($"Comment with id {id} was not found.");
		}
	}

	private async Task ValidateAsync(Comment comment, Guid? updatingId = null)
	{
		if (string.IsNullOrWhiteSpace(comment.Description))
		{
			throw new ValidationException("Comment description is required.");
		}

		if (await userRepository.GetByIdAsync(comment.UserId) is null)
		{
			throw new EntityNotFoundException($"User with id {comment.UserId} was not found.");
		}

		if (await postRepository.GetByIdAsync(comment.PostId) is null)
		{
			throw new EntityNotFoundException($"Post with id {comment.PostId} was not found.");
		}

		if (!comment.ParentId.HasValue)
		{
			return;
		}

		if (comment.ParentId == updatingId)
		{
			throw new ValidationException("Comment cannot be parent of itself.");
		}

		var parent = await repository.GetByIdAsync(comment.ParentId.Value)
			?? throw new EntityNotFoundException($"Parent comment with id {comment.ParentId.Value} was not found.");

		if (parent.PostId != comment.PostId)
		{
			throw new ValidationException("Parent comment must belong to the same post.");
		}
	}

	private static List<Expression<Func<T, object>>> EmptyIncludes<T>() where T : BaseEntity => [];
}