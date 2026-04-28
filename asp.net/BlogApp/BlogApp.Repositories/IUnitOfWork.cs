namespace BlogApp.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IPostRepository Posts { get; }
    ICommentRepository Comments { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}