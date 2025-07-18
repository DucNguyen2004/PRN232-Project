namespace DTOs
{
    public class ProductRequestDto
    {
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public required string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreateAt { get; set; }
        public required string Status { get; set; }
        public ICollection<string> Images { get; set; } = new List<string>();
        public ICollection<ProductOptionRequestDto> Options { get; set; } = new List<ProductOptionRequestDto>();
    }
}
