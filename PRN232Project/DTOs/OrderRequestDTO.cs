namespace DTOs
{
    public class OrderRequestDTO
    {
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string Message { get; set; }
        public double DiscountPrice { get; set; }
    }
}
