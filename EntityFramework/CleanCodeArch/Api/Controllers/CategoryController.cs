using Api.DTOs.Category;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

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
		var created = await categoryService.CreateAsync(request.ToEntity());
		return Ok(created.ToResponse());
	}
}