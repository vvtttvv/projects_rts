using Domain.Entities;

namespace Repositories.Interfaces;

public interface ICategoryRepository
{
	Task<IReadOnlyCollection<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
	Task<Category> AddAsync(Category category);
}