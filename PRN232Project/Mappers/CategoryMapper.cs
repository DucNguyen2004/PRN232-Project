using BusinessObjects;
using DTOs;

namespace Mappers
{
    public class CategoryMapper
    {
        public static CategoryResponseDto ToDto(Category entity)
        {
            return new CategoryResponseDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
