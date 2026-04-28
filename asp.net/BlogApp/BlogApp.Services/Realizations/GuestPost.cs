using System.Threading;
using BlogApp.Services.Interfaces;
using BlogApp.Repositories;
using BlogApp.Domain.Entities;

namespace BlogApp.Services.Realizations;

public class GuestPost(
	IUserRepository userRepository,
	IPostRepository postRepository) : IGuestPost
{
	public Task<bool> CreateUserAsync(User user)
	{
		return AddEntityAsync(userRepository, user);
	}

	public Task<bool> AddPostAsync(Post post)
	{
		return AddEntityAsync(postRepository, post);
	}

	public async Task SaveAsync(CancellationToken ct = default)
	{
		await userRepository.SaveChangesAsync(ct);
	}

	public async Task<bool> AddUserAndPostAsync(User user, Post post, CancellationToken ct = default)
	{
		await userRepository.AddAsync(user);
		await postRepository.AddAsync(post);
		await userRepository.SaveChangesAsync(ct);
		return true;
	}

	private static async Task<bool> AddEntityAsync<T>(IGenericRepository<T> repository, T entity)
		where T : BaseEntity
	{
		await repository.AddAsync(entity);
		return true;
	}
}
