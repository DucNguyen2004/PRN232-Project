namespace DTOs
{
    public class CartItemRequestDto
    {
        public int ProductId { get; set; }
        public int ProductOptionId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
