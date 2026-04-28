using System.Threading;
using BlogApp.Services.Interfaces;
using BlogApp.Repositories;
using BlogApp.Domain.Entities;

namespace BlogApp.Services.Realizations;

public class GuestPost(
	IUserRepository userRepository,
	IPostRepository postRepository) : IGuestPost
{
	private User? _pendingUser;
	private Post? _pendingPost;

	public IUserRepository User => userRepository;
	public IPostRepository Post => postRepository;

	public Task<bool> CreateUserAsync(User user)
	{
		_pendingUser = user;
		return Task.FromResult(true);
	}

	public Task<bool> AddPostAsync(Post post)
	{
		_pendingPost = post;
		return Task.FromResult(true);
	}

	public async Task SaveAsync(CancellationToken ct = default)
	{
		if (_pendingUser is not null)
		{
			await User.AddAsync(_pendingUser);
		}

		if (_pendingPost is not null)
		{
			await Post.AddAsync(_pendingPost);
		}

		if (_pendingUser is not null || _pendingPost is not null)
		{
			await User.SaveChangesAsync(ct);
		}

		_pendingUser = null;
		_pendingPost = null;
	}

	public async Task<bool> AddUserAndPostAsync(User user, Post post, CancellationToken ct = default)
	{
		_pendingUser = user;
		_pendingPost = post;
		await SaveAsync(ct);
		return true;
	}
}
