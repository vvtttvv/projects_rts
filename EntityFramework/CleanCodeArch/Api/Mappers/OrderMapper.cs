using Api.DTOs.Order;
using Domain.Entities;

namespace Api.Mappers;

public static class OrderMapper
{
	public static Order ToEntity(this OrderRequestDto dto) => new()
	{
		ProductId = dto.ProductId,
		Quantity = dto.Quantity
	};

	public static OrderResponseDto ToResponse(this Order entity) => new()
	{
		Id = entity.Id,
		ProductId = entity.ProductId,
		Quantity = entity.Quantity
	};
}