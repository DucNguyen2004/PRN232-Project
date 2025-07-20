namespace DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public UserResponseDto User { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string Message { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderDetailResponseDTO> OrderDetails { get; set; }
        public double DiscountPrice { get; set; }
    }
}
