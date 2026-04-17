using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private static readonly List<Category> Categories = [];
	private static int _nextId = 1;

	public Task<IReadOnlyCollection<Category>> GetAllAsync() => Task.FromResult<IReadOnlyCollection<Category>>(Categories);

	public Task<Category?> GetByIdAsync(int id) => Task.FromResult(Categories.FirstOrDefault(x => x.Id == id));

	public Task<Category> AddAsync(Category category)
	{
		category.Id = _nextId++;
		Categories.Add(category);
		return Task.FromResult(category);
	}
}