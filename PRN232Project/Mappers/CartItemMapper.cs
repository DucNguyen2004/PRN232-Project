using BusinessObjects;
using DTOs;

namespace Mappers
{
    public class CartItemMapper
    {
        public static CartItemResponseDto ToDTO(CartItem cartItem)
        {
            if (cartItem == null) return null;

            return new CartItemResponseDto
            {
                Id = cartItem.Id,
                Quantity = cartItem.Quantity,
                //Product = cartItem.Product != null ? _productMapper.ToResponseDTO(cartItem.Product) : null, // modify later
                User = cartItem.User != null ? UserMapper.ToDTO(cartItem.User) : null
            };
        }

        public static CartItem ToEntity(CartItemRequestDto requestDTO)
        {
            if (requestDTO == null) return null;

            return new CartItem
            {
                Quantity = requestDTO.Quantity,
                UserId = requestDTO.UserId,
                ProductId = requestDTO.Product?.Id ?? 0
            };
        }
    }
}
