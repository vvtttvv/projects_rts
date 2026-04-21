using Store.Api.DTOs.Models.Category;

namespace Store.Api.DTOs.Mappers;

public static class CategoryMapper
{
	public static Domain.Entities.Category ToEntity(this CategoryRequestDto dto) => new()
	{
		Name = dto.Name
	};

	public static CategoryResponseDto ToResponse(this Domain.Entities.Category entity) => new()
	{
		Id = entity.Id,
		Name = entity.Name
	};
}