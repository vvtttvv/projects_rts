using Microsoft.AspNetCore.Mvc;
using Store.Api.DTOs.Mappers;
using Store.Api.DTOs.Models.Product;
using Store.Services.Exceptions;
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

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
	{
		var result = await productService.GetByIdAsync(id);
		return result is null ? NotFound() : Ok(result.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<ProductResponseDto>> Create(ProductRequestDto request)
	{
		try
		{
			var created = await productService.CreateAsync(request.ToEntity());
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (EntityNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ProductResponseDto>> Update(Guid id, ProductRequestDto request)
	{
		try
		{
			var updated = await productService.UpdateAsync(id, request.ToEntity());
			return Ok(updated.ToResponse());
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (EntityNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		try
		{
			await productService.DeleteAsync(id);
			return NoContent();
		}
		catch (EntityNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (ConflictException ex)
		{
			return Conflict(ex.Message);
		}
	}
}