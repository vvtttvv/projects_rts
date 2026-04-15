using Store.Api.DTOs.Category;
using Store.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
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

	[HttpPost]
	public async Task<ActionResult<CategoryResponseDto>> Create(CategoryRequestDto request)
	{
		try
		{
			var created = await categoryService.CreateAsync(request.ToEntity());
			return Ok(created.ToResponse());
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (InvalidOperationException ex)
		{
			return Conflict(ex.Message);
		}
	}
}