namespace DTOs
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public UserResponseDto User { get; set; }
        public ProductResponseDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
