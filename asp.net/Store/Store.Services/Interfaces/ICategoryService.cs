using Store.Domain.Entities;

namespace Store.Services.Interfaces;

public interface ICategoryService
{
	Task<IReadOnlyCollection<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(Guid id);
	Task<Category> CreateAsync(Category category);
	Task<Category> UpdateAsync(Guid id, Category category);
	Task DeleteAsync(Guid id);
}