using System.Linq.Expressions;

namespace Store.Repositories.Models;

public class QueryOptions<TEntity>
{
	public int? StartIndex { get; init; }
	public int? MaxCount { get; init; }
	public bool AsNoTracking { get; init; } = true;
	public Expression<Func<TEntity, bool>>? Filter { get; init; }
	public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; }
	public List<Expression<Func<TEntity, object>>> Includes { get; init; } = [];
}

