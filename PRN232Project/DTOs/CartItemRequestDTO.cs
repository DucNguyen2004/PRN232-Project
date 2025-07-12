namespace DTOs
{
    public class CartItemRequestDTO
    {
        public ProductRequestDTO Product { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
