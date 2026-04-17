using Store.Domain.Entities;

namespace Store.Repositories.Interfaces;

public interface ICategoryRepository
{
	Task<IReadOnlyCollection<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
	Task<Category> AddAsync(Category category);
}