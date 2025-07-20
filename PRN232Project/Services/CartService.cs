using BusinessObjects;
using DTOs;
using Mappers;
using Repositories;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _productRepo;

        public async Task<IEnumerable<CartItemResponseDto>> GetAllCartItems(int userId) // change parameter ?
        {
            var cartItems = await _cartItemRepo.GetAllCartItems(userId);

            return cartItems.Select(x => CartItemMapper.ToDTO(x));
        }

        public async Task<CartItemResponseDto> AddToCart(CartItemRequestDto requestDTO)
        {
            if (await _cartItemRepo.ExistsAsync(requestDTO.UserId, requestDTO.Product.Id))
                return null;

            var item = new CartItem
            {
                UserId = requestDTO.UserId,
                ProductId = requestDTO.Product.Id,
                Quantity = requestDTO.Quantity,
            };

            var cartItem = await _cartItemRepo.AddToCartAsync(item);

            return CartItemMapper.ToDTO(cartItem);
        }

        public async Task<CartItem?> UpdateQuantity(int cartItemId, int quantity)
        {
            var item = await _cartItemRepo.GetByIdAsync(cartItemId);
            if (item == null) return null;

            item.Quantity = quantity;
            await _cartItemRepo.UpdateAsync(item);
            return item;
        }

        public async Task DeleteCartItem(int cartItemId)
        {
            await _cartItemRepo.DeleteAsync(cartItemId);
        }
        public async Task ClearCart(int userId)
        {
            await _cartItemRepo.DeleteByUserIdAsync(userId);
        }
    }
}
