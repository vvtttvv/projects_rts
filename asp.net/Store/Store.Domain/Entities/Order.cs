namespace Store.Domain.Entities;

public class Order : BaseEntity
{
	public int Quantity { get; set; }
	public Guid ProductId { get; set; }

	public virtual Product Product { get; set; } = null!;
}