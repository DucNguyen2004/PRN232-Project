using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232Project.Entities
{
    [Table("discount")]
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CouponId { get; set; }

        [ForeignKey("CouponId")]
        public Coupon Coupon { get; set; }

        public string Type { get; set; } // e.g., category, product, total, ship

        public long? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public long? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string ValueType { get; set; } // e.g., percent, fixed

        public double? ValueFixed { get; set; }
    }
}
