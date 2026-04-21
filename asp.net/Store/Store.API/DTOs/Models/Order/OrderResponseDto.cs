namespace Store.Api.DTOs.Models.Order;

public class OrderResponseDto
{
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
}