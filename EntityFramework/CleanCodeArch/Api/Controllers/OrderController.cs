using Api.DTOs.Order;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

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
		var created = await orderService.CreateAsync(request.ToEntity());
		return Ok(created.ToResponse());
	}
}