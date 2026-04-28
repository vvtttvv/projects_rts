using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services.Realizations;

public class PostService(IPostRepository repository, IUserRepository userRepository) : IPostService
{
	public Task<PagedResult<Post>> GetAllAsync(int page = 1, int pageSize = 10) =>
		repository.GetAllAsync(page, pageSize);

	public Task<Post?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Post> CreateAsync(Post post)
	{
		await ValidateAsync(post);
		post.Title = post.Title.Trim();
		post.Description = post.Description.Trim();
		var created = await repository.AddAsync(post);
		await repository.SaveChangesAsync();
		return created;
	}

	public async Task<Post> UpdateAsync(Guid id, Post post)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"Post with id {id} was not found.");

		await ValidateAsync(post, id);

		current.Title = post.Title.Trim();
		current.Description = post.Description.Trim();
		current.UserId = post.UserId;

		var updated = await repository.UpdateByIdAsync(id, current)
			?? throw new EntityNotFoundException($"Post with id {id} was not found.");

		await repository.SaveChangesAsync();
		return updated;
	}

	public async Task DeleteAsync(Guid id)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"Post with id {id} was not found.");

		var deleted = await repository.DeleteAsync(current.Id);
		if (!deleted)
		{
			throw new EntityNotFoundException($"Post with id {id} was not found.");
		}

		await repository.SaveChangesAsync();
	}

	private async Task ValidateAsync(Post post, Guid? updatingId = null)
	{
		if (string.IsNullOrWhiteSpace(post.Title))
		{
			throw new ValidationException("Post title is required.");
		}

		var user = await userRepository.GetByIdAsync(post.UserId);
		if (user is null)
		{
			throw new EntityNotFoundException($"User with id {post.UserId} was not found.");
		}

		var normalizedTitle = post.Title.Trim();
		if (await repository.ExistsByUserAndTitleAsync(post.UserId, normalizedTitle, updatingId))
		{
			throw new ConflictException($"Post with title '{normalizedTitle}' already exists for this user.");
		}
	}

}
