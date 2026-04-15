using Store.Api.DTOs.Order;
using Store.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
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

	[HttpPost]
	public async Task<ActionResult<OrderResponseDto>> Create(OrderRequestDto request)
	{
		try
		{
			var created = await orderService.CreateAsync(request.ToEntity());
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