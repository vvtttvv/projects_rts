namespace BlogApp.Domain.Entities;

public class Post : BaseEntity
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}