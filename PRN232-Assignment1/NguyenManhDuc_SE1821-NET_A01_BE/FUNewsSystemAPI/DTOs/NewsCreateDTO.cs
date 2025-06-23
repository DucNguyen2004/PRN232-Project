namespace FUNewsSystemAPI.DTOs
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public short CreatedById { get; set; }
        public short UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
