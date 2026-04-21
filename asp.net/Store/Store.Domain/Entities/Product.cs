namespace Store.Domain.Entities;

public class Product : BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public Guid CategoryId { get; set; }
	
	public virtual Category Category { get; set; } = null!;
}