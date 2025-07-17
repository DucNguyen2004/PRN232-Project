using BusinessObjects;
using DTOs;

namespace Mappers
{
    public class ProductMapper
    {
        public static ProductResponseDto ToDto(Product entity)
        {
            return new ProductResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = CategoryMapper.ToDto(entity.Category),
                Description = entity.Description,
                Price = entity.Price,
                CreateAt = entity.CreateAt,
                Sold = entity.Sold,
                Status = entity.Status,
                Images = entity.ProductImages.Select(img => img.Image).ToList(),
                Options = entity.ProductOptions.Select(ProductOptionMapper.ToDto).ToList()
            };
        }
    }
}
