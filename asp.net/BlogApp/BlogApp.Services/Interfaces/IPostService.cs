using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Services.Interfaces;

public interface IPostService
{
	Task<PagedResult<Post>> GetAllAsync(int page = 1, int pageSize = 10);
	Task<Post?> GetByIdAsync(Guid id);
	Task<Post> CreateAsync(Post post);
	Task<Post> UpdateAsync(Guid id, Post post);
	Task DeleteAsync(Guid id);
}