using BlogApp.Repositories;
using BlogApp.Domain.Entities;

namespace BlogApp.Services.Interfaces;

public interface IGuestPost
{
    IUserRepository User { get; }
    IPostRepository Post { get; }
    Task<bool> CreateUserAsync(User user);
    Task<bool> AddPostAsync(Post post);
    Task SaveAsync(CancellationToken ct = default);
    Task<bool> AddUserAndPostAsync(User user, Post post, CancellationToken ct = default);
}