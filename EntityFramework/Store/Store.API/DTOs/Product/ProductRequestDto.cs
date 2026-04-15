namespace Store.Api.DTOs.Product;

public class ProductRequestDto
{
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public int CategoryId { get; set; }
}