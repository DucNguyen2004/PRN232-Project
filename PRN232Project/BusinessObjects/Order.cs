﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public string Message { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? CouponId { get; set; }
        [ForeignKey("CouponId")]
        public Coupon? Coupon { get; set; }

        public double DiscountPrice { get; set; }
        public string CancelReason { get; set; }
    }
}
