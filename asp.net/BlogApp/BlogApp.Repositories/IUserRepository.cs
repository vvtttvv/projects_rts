using BlogApp.Domain.Entities;

namespace BlogApp.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
	Task<bool> ExistsByUserNameAsync(string userName, Guid? excludingId = null);
}

