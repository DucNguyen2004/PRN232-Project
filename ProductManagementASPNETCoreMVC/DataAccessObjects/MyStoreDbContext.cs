using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
    public partial class MyStoreDbContext : DbContext
    {
        public MyStoreDbContext()
        {
        }

        public MyStoreDbContext(DbContextOptions<MyStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountNumber> AccountNumbers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(GetConnectionString());

        private string GetConnectionString()
        {
            var direct = Directory.GetCurrentDirectory();
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:MyStockDB"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountNumber>(entity =>
            {
                entity.HasKey(e => e.MemberId).HasName("PK__AccountN__0CF04B382436838A");

                entity.ToTable("AccountNumber");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");
                entity.Property(e => e.EmailAddress).HasMaxLength(100);
                entity.Property(e => e.FullName).HasMaxLength(80);
                entity.Property(e => e.MemberPassword).HasMaxLength(80);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B14F5DD9B");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasMaxLength(15);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED3936CB16");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.ProductName).HasMaxLength(40);
                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


