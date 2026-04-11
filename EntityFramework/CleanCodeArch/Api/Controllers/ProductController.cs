using Api.DTOs.Product;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IReadOnlyCollection<ProductResponseDto>>> GetAll()
	{
		var result = await productService.GetAllAsync();
		return Ok(result.Select(x => x.ToResponse()));
	}

	[HttpPost]
	public async Task<ActionResult<ProductResponseDto>> Create(ProductRequestDto request)
	{
		var created = await productService.CreateAsync(request.ToEntity());
		return Ok(created.ToResponse());
	}
}