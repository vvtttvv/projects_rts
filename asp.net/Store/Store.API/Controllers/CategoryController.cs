using Microsoft.AspNetCore.Mvc;
using Store.Api.DTOs.Mappers;
using Store.Api.DTOs.Models.Category;
using Store.Services.Exceptions;
using Store.Services.Interfaces;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IReadOnlyCollection<CategoryResponseDto>>> GetAll()
	{
		var result = await categoryService.GetAllAsync();
		return Ok(result.Select(x => x.ToResponse()));
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
	{
		var result = await categoryService.GetByIdAsync(id);
		return result is null ? NotFound() : Ok(result.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<CategoryResponseDto>> Create(CategoryRequestDto request)
	{
		try
		{
			var created = await categoryService.CreateAsync(request.ToEntity());
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (ConflictException ex)
		{
			return Conflict(ex.Message);
		}
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<CategoryResponseDto>> Update(Guid id, CategoryRequestDto request)
	{
		try
		{
			var updated = await categoryService.UpdateAsync(id, request.ToEntity());
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
		catch (ConflictException ex)
		{
			return Conflict(ex.Message);
		}
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		try
		{
			await categoryService.DeleteAsync(id);
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