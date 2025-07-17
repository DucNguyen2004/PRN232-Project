namespace DTOs
{
    public class ProductOptionResponseDto
    {
        public required string Type { get; set; }
        public required string Name { get; set; }
        public int DeltaPrice { get; set; }
    }
}
