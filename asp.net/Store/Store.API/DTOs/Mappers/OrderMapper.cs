using Store.Api.DTOs.Models.Order;

namespace Store.Api.DTOs.Mappers;

public static class OrderMapper
{
	public static Domain.Entities.Order ToEntity(this OrderRequestDto dto) => new()
	{
		ProductId = dto.ProductId,
		Quantity = dto.Quantity
	};

	public static OrderResponseDto ToResponse(this Domain.Entities.Order entity) => new()
	{
		Id = entity.Id,
		ProductId = entity.ProductId,
		Quantity = entity.Quantity
	};
}