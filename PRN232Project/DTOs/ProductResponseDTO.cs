namespace DTOs
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; }
        public string PrevStatus { get; set; }
    }
}
