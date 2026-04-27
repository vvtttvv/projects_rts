using BlogApp.Domain.Entities;

namespace BlogApp.Repositories;

public interface IPostRepository : IGenericRepository<Post>
{
	Task<bool> ExistsByUserAndTitleAsync(Guid userId, string title, Guid? excludingId = null);
}

