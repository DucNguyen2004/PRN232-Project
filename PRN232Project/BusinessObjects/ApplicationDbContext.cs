using Microsoft.EntityFrameworkCore;

namespace BusinessObjects
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for all your entities
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between User and Role
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("user_roles"));

            // Role Configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description);
            });

            // UserAddress Configuration
            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Address).HasMaxLength(256).IsRequired();
                entity.Property(e => e.IsDefault).IsRequired();

                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.Addresses)
                      .HasForeignKey(ua => ua.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(true);
            });

            // Product Configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(256).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.CreateAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Sold).HasDefaultValue(0);
                entity.Property(e => e.Status);
                entity.Property(e => e.PrevStatus);

                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ProductImage Configuration
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Image).HasMaxLength(256).IsRequired();

                entity.HasOne(pi => pi.Product)
                      .WithMany(p => p.ProductImages)
                      .HasForeignKey(pi => pi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Option Configuration
            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            // OptionValue Configuration
            modelBuilder.Entity<OptionValue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);

                entity.HasOne(ov => ov.Option)
                      .WithMany(o => o.OptionValues)
                      .HasForeignKey(ov => ov.OptionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ProductOption Configuration
            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(po => po.Product)
                      .WithMany(p => p.ProductOptions)
                      .HasForeignKey(po => po.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(po => po.Option)
                      .WithMany(o => o.ProductOptions)
                      .HasForeignKey(po => po.OptionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CartItem Configuration
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne(ci => ci.User)
                      .WithMany()
                      .HasForeignKey(ci => ci.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Product)
                      .WithMany(p => p.CartItems)
                      .HasForeignKey(ci => ci.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Ensure unique cart item per user-product combination
                entity.HasIndex(ci => new { ci.UserId, ci.ProductId }).IsUnique();
            });

            // WishlistItem Configuration
            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(wi => wi.User)
                      .WithMany()
                      .HasForeignKey(wi => wi.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wi => wi.Product)
                      .WithMany(p => p.WishlistItems)
                      .HasForeignKey(wi => wi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Ensure unique wishlist item per user-product combination
                entity.HasIndex(wi => new { wi.UserId, wi.ProductId }).IsUnique();
            });

            // Coupon Configuration
            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Status);
                entity.Property(e => e.Code).HasMaxLength(255).IsRequired();
                entity.Property(e => e.DateCreated);
                entity.Property(e => e.DateExpired);
                entity.Property(e => e.DateUpdated);
                entity.Property(e => e.DateValid);
                entity.Property(e => e.Description);
                entity.Property(e => e.LimitAccountUses).HasDefaultValue(0);
                entity.Property(e => e.LimitUses).HasDefaultValue(0);
                entity.Property(e => e.UseCount).HasDefaultValue(0);

                // Ensure unique coupon code
                entity.HasIndex(c => c.Code).IsUnique();
            });

            // Discount Configuration
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type);
                entity.Property(e => e.ValueType);
                entity.Property(e => e.ValueFixed);

                entity.HasOne(d => d.Coupon)
                      .WithMany(c => c.Discounts)
                      .HasForeignKey(d => d.CouponId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Category)
                      .WithMany()
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Product)
                      .WithMany()
                      .HasForeignKey(d => d.ProductId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderDate).IsRequired();
                entity.Property(e => e.ShippingAddress).IsRequired();
                entity.Property(e => e.OrderStatus).IsRequired();
                entity.Property(e => e.Message);
                entity.Property(e => e.DiscountPrice).HasDefaultValue(0);
                entity.Property(e => e.CancelReason);

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Coupon)
                      .WithMany()
                      .HasForeignKey(o => o.CouponId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // OrderDetail Configuration
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.price).IsRequired();

                entity.HasOne(od => od.Order)
                      .WithMany(o => o.OrderDetails)
                      .HasForeignKey(od => od.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(od => od.Product)
                      .WithMany()
                      .HasForeignKey(od => od.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // RefreshToken Configuration
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.Expiry).IsRequired();

                entity.HasOne(rt => rt.User)
                      .WithMany()
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Ensure unique token
                entity.HasIndex(rt => rt.Token).IsUnique();
            });

            // Feedback Configuration
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.SentAt).IsRequired();

                // Note: IsRead is private in your model, you may need to make it public
                // or configure it differently if you want to map it to the database
            });
        }
    }
}
