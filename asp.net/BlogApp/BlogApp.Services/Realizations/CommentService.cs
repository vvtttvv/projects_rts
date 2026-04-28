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
	public Task<PagedResult<Comment>> GetAllAsync(int page = 1, int pageSize = 10) =>
		repository.GetAllAsync(page, pageSize);

	public Task<Comment?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Comment> CreateAsync(Comment comment)
	{
		await ValidateAsync(comment);
		comment.Description = comment.Description.Trim();
		var created = await repository.AddAsync(comment);
		await repository.SaveChangesAsync();
		return created;
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

		var updated = await repository.UpdateByIdAsync(id, current)
			?? throw new EntityNotFoundException($"Comment with id {id} was not found.");

		await repository.SaveChangesAsync();
		return updated;
	}

	public async Task DeleteAsync(Guid id)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"Comment with id {id} was not found.");

		var deleted = await repository.DeleteAsync(current.Id);
		if (!deleted)
		{
			throw new EntityNotFoundException($"Comment with id {id} was not found.");
		}

		await repository.SaveChangesAsync();
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

}