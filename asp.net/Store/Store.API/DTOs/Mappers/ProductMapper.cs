using Store.Api.DTOs.Models.Product;

namespace Store.Api.DTOs.Mappers;

public static class ProductMapper
{
	public static Domain.Entities.Product ToEntity(this ProductRequestDto dto) => new()
	{
		Name = dto.Name,
		Price = dto.Price,
		CategoryId = dto.CategoryId
	};

	public static ProductResponseDto ToResponse(this Domain.Entities.Product entity) => new()
	{
		Id = entity.Id,
		Name = entity.Name,
		Price = entity.Price
	};
}