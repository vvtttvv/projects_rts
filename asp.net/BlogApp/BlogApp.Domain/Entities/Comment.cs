namespace BlogApp.Domain.Entities;

public class Comment : BaseEntity
{
    public required string Description { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public Guid? ParentId { get; set; }
    
    public virtual Comment? Parent { get; set; }
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
    
    public virtual User User { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;
}
