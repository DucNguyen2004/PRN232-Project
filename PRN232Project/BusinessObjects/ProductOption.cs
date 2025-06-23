using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("product_options")]
    public class ProductOption
    {
        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public long OptionId { get; set; }

        [ForeignKey("OptionId")]
        public Option Option { get; set; }
    }
}
