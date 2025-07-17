using BusinessObjects;
using DTOs;

namespace Mappers
{
    public class ProductOptionMapper
    {
        public static ProductOptionResponseDto ToDto(ProductOption entity)
        {
            return new ProductOptionResponseDto
            {
                Type = entity.OptionValue.Option.Name,
                Name = entity.OptionValue.Name,
                DeltaPrice = entity.DeltaPrice
            };
        }
    }
}
