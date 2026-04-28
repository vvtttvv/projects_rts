using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Services.Interfaces;

public interface IUserService
{
	Task<PagedResult<User>> GetAllAsync(int page = 1, int pageSize = 10);
	Task<User?> GetByIdAsync(Guid id);
	Task<User> CreateAsync(User user);
	Task<User> UpdateAsync(Guid id, User user);
	Task DeleteAsync(Guid id);
}