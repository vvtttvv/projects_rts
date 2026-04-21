namespace Store.Api.DTOs.Models.Order;

public class OrderRequestDto
{
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
}