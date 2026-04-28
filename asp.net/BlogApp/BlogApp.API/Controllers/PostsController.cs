using BlogApp.API.DTO.Mappers;
using BlogApp.API.DTO.Models.Posts;
using BlogApp.Repositories;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService service) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(typeof(PagedResult<PostResponse>), StatusCodes.Status200OK)]
	public async Task<ActionResult<PagedResult<PostResponse>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		var posts = await service.GetAllAsync(page, pageSize);
		return Ok(new PagedResult<PostResponse>(
			posts.Items.Select(x => x.ToResponse()).ToArray(),
			posts.Page,
			posts.PageSize,
			posts.TotalCount));
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<PostResponse>> GetById(Guid id)
	{
		var post = await service.GetByIdAsync(id);
		if (post is null)
		{
			return NotFound();
		}

		return Ok(post.ToResponse());
	}

	[HttpPost]
	[ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<PostResponse>> Create([FromBody] CreatePostRequest request)
	{
		var created = await service.CreateAsync(request.ToEntity());
		var response = created.ToResponse();
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<PostResponse>> Update(Guid id, [FromBody] UpdatePostRequest request)
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
