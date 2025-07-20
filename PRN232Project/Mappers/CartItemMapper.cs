using BusinessObjects;
using DTOs;

namespace Mappers
{
    public class CartItemMapper
    {
        public static CartItemResponseDto ToDTO(CartItem entity)
        {
            return new CartItemResponseDto
            {
                Id = entity.Id,
                Product = new CartItemProductDto
                {
                    Id = entity.Product.Id,
                    Name = entity.Product.Name,
                    Price = entity.Product.Price,
                    Status = entity.Product.Status,
                    Image = entity.Product.ProductImages?.FirstOrDefault()?.Image ?? string.Empty
                },
                ProductOption = entity.ProductOption != null ? ProductOptionMapper.ToDTO(entity.ProductOption) : null,
                Quantity = entity.Quantity
            };
        }

        public static CartItem ToEntity(CartItemRequestDto dto)
        {
            return new CartItem
            {
                Quantity = dto.Quantity,
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                ProductOptionId = dto.ProductOptionId
            };
        }
    }
}
