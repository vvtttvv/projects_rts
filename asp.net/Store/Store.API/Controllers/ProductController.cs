using Store.Api.DTOs.Product;
using Store.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Interfaces;

namespace Store.Api.Controllers;

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
		try
		{
			var created = await productService.CreateAsync(request.ToEntity());
			return Ok(created.ToResponse());
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}
}