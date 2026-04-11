using Api.DTOs.Category;
using Domain.Entities;

namespace Api.Mappers;

public static class CategoryMapper
{
	public static Category ToEntity(this CategoryRequestDto dto) => new()
	{
		Name = dto.Name
	};

	public static CategoryResponseDto ToResponse(this Category entity) => new()
	{
		Id = entity.Id,
		Name = entity.Name
	};
}