using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class ProductRepository : IProductRepository
{
	private static readonly List<Product> Products = [];
	private static int _nextId = 1;

	public Task<IReadOnlyCollection<Product>> GetAllAsync() => Task.FromResult<IReadOnlyCollection<Product>>(Products);

	public Task<Product?> GetByIdAsync(int id) => Task.FromResult(Products.FirstOrDefault(x => x.Id == id));

	public Task<Product> AddAsync(Product product)
	{
		product.Id = _nextId++;
		Products.Add(product);
		return Task.FromResult(product);
	}
}