namespace DTOs
{
    public class OrderDetailResponseDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public ProductResponseDTO Product { get; set; }
    }
}
