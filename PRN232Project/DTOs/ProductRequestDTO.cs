using BusinessObjects;

namespace DTOs
{
    public class ProductRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ImageList { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; } // re-check
        public DateTime CreateAt { get; set; }
        public List<long> OptionList { get; set; }
        public string Status { get; set; }
        public string PrevStatus { get; set; }
        private User User { get; set; }
    }
}
