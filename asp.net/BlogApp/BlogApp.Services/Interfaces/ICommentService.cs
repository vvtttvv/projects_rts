using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Services.Interfaces;

public interface ICommentService
{
	Task<PagedResult<Comment>> GetAllAsync(int page = 1, int pageSize = 10);
	Task<Comment?> GetByIdAsync(Guid id);
	Task<Comment> CreateAsync(Comment comment);
	Task<Comment> UpdateAsync(Guid id, Comment comment);
	Task DeleteAsync(Guid id);
}