using BlogApp.Repositories;

namespace BlogApp.Postgres.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db, IUserRepository users, IPostRepository posts, ICommentRepository comments)
    {
        _db = db;
        Users = users;
        Posts = posts;
        Comments = comments;
    }

    public IUserRepository Users { get; }
    public IPostRepository Posts { get; }
    public ICommentRepository Comments { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}