using Microsoft.AspNetCore.Mvc;
using Store.Api.DTOs.Mappers;
using Store.Api.DTOs.Models.Order;
using Store.Services.Exceptions;
using Store.Services.Interfaces;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IReadOnlyCollection<OrderResponseDto>>> GetAll()
	{
		var result = await orderService.GetAllAsync();
		return Ok(result.Select(x => x.ToResponse()));
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<OrderResponseDto>> GetById(Guid id)
	{
		var result = await orderService.GetByIdAsync(id);
		return result is null ? NotFound() : Ok(result.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<OrderResponseDto>> Create(OrderRequestDto request)
	{
		try
		{
			var created = await orderService.CreateAsync(request.ToEntity());
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
	public async Task<ActionResult<OrderResponseDto>> Update(Guid id, OrderRequestDto request)
	{
		try
		{
			var updated = await orderService.UpdateAsync(id, request.ToEntity());
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
			await orderService.DeleteAsync(id);
			return NoContent();
		}
		catch (EntityNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}
}