using System.Security.Claims;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Utils;
using Services;

namespace PRN232Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ApiControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<UserCartResponseDto>>> GetCart()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw ProblemException.BadRequest("Invalid or missing user ID in token.");
            }

            var cart = await _cartService.GetAllCartItems(userId);

            if (cart == null)
            {
                throw ProblemException.NotFound("No cart item found from user ID: " + userId);
            }

            return OkResponse(cart);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<CartItemResponseDto>>> GetCartItemById(int id)
        {
            var cartItem = await GetEntityOrThrowAsync(
                () => _cartService.GetCartItemById(id),
                id,
                nameof(User));

            return OkResponse(cartItem);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CartItemResponseDto>>> AddToCart([FromBody] CartItemRequestDto dto)
        {
            if (dto == null)
            {
                throw ProblemException.BadRequest("Invalid cart item data.");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw ProblemException.BadRequest("Invalid or missing user ID in token.");
            }

            if (await _cartService.IsCartItemExisted(userId, dto.ProductId, dto.ProductOptionIds))
            {
                throw ProblemException.BadRequest("This product with selected options is already in the cart.");
            }

            CartItem cartItem = await _cartService.AddToCart(dto, userId);
            return CreatedResponse(nameof(GetCartItemById), new { id = cartItem.Id }, Mappers.CartItemMapper.ToDTO(cartItem));
        }

        [HttpPut("{cartItemId:int}")]
        public async Task<ActionResult> UpdateCartItemQuantity(int cartItemId, [FromBody] int newQuantity)
        {
            await GetEntityOrThrowAsync(
                () => _cartService.GetCartItemById(cartItemId),
                cartItemId,
                nameof(CartItem));

            await _cartService.UpdateQuantity(cartItemId, newQuantity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCartItem(int id)
        {
            await GetEntityOrThrowAsync(
                () => _cartService.GetCartItemById(id),
                id,
                nameof(CartItem));

            await _cartService.DeleteCartItem(id);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCart()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw ProblemException.BadRequest("Invalid or missing user ID in token.");
            }

            await _cartService.ClearCart(userId);
            return NoContent();
        }
    }
}

