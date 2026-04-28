using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Users;
using BlogApp.Repositories;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService service) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(typeof(PagedResult<UserResponse>), StatusCodes.Status200OK)]
	public async Task<ActionResult<PagedResult<UserResponse>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		var users = await service.GetAllAsync(page, pageSize);
		return Ok(new PagedResult<UserResponse>(
			users.Items.Select(x => x.ToResponse()).ToArray(),
			users.Page,
			users.PageSize,
			users.TotalCount));
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<UserResponse>> GetById(Guid id)
	{
		var user = await service.GetByIdAsync(id);
		if (user is null)
		{
			return NotFound();
		}

		return Ok(user.ToResponse());
	}

	[HttpPost]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserRequest request)
	{
		var created = await service.CreateAsync(request.ToEntity());
		var response = created.ToResponse();
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<UserResponse>> Update(Guid id, [FromBody] UpdateUserRequest request)
	{
		var updated = await service.UpdateAsync(id, request.ToEntity());
		return Ok(updated.ToResponse());
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(Guid id)
	{
		await service.DeleteAsync(id);
		return NoContent();
	}
}
