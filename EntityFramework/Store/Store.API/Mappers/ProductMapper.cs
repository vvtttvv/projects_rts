using Store.Api.DTOs.Product;
using Store.Domain.Entities;

namespace Store.Api.Mappers;

public static class ProductMapper
{
	public static Product ToEntity(this ProductRequestDto dto) => new()
	{
		Name = dto.Name,
		Price = dto.Price,
		CategoryId = dto.CategoryId
	};

	public static ProductResponseDto ToResponse(this Product entity) => new()
	{
		Id = entity.Id,
		Name = entity.Name,
		Price = entity.Price
	};
}