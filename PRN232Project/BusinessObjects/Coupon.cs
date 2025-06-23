using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("coupon")]
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        [Required]
        [MaxLength(255)]
        public string Code { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateExpired { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime? DateValid { get; set; }

        public string Description { get; set; }

        public int LimitAccountUses { get; set; }

        public int LimitUses { get; set; }

        public int UseCount { get; set; }

        // One-to-Many: Coupon -> Discounts
        public ICollection<Discount> Discounts { get; set; }
    }
}
