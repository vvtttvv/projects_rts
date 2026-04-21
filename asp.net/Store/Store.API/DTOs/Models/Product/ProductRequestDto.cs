namespace Store.Api.DTOs.Models.Product;

public class ProductRequestDto
{
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public Guid CategoryId { get; set; }
}