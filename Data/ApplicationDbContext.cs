namespace api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<BaseUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponUsage> CouponUsages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ExtraInformation> ExtraInformations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Tables
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<DeliveryPerson>().ToTable("DeliveryPersons");
            modelBuilder.Entity<Admin>().ToTable("Admins");

            // Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of a brand if products exist

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.BrandId);

            // SubCategory
            modelBuilder.Entity<SubCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of a category if subcategories exist

            modelBuilder.Entity<SubCategory>()
                .HasIndex(sc => sc.CategoryId);

            // Brand
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.SubCategory)
                .WithMany(sc => sc.Brands)
                .HasForeignKey(b => b.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of subcategory if brands exist

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.SubCategoryId);

            // ExtraInformation
            modelBuilder.Entity<ExtraInformation>()
                .HasOne(e => e.Product)
                .WithMany(p => p.Informations)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Delete ExtraInformation when Product is deleted

            modelBuilder.Entity<ExtraInformation>()
                .HasIndex(e => e.ProductId);

            // Cart
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete Cart when User is deleted

            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Delete Cart when Product is deleted

            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.ProductId);

            // Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Delete reviews when the product is deleted

            modelBuilder.Entity<Review>()
                .HasIndex(r => r.ProductId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete reviews when the user is deleted

            modelBuilder.Entity<Review>()
                .HasIndex(r => r.UserId);

            // Wishlist
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete wishlists when the user is deleted

            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => w.UserId);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Delete wishlists when the product is deleted

            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => w.ProductId);

            // OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete OrderItems when the Order is deleted

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of product if it is used in an order

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.ProductId);

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete orders when user is deleted

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent address deletion if it is referenced in orders

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.AddressId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Coupon)
                .WithOne()
                .HasForeignKey<Order>(o => o.CouponId)
                .OnDelete(DeleteBehavior.SetNull); // Set null if Coupon is deleted

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithOne()
                .HasForeignKey<Order>(o => o.DeliveryId)
                .OnDelete(DeleteBehavior.SetNull); // Set null if Delivery is deleted

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne()
                .HasForeignKey<Order>(o => o.PaymentId)
                .OnDelete(DeleteBehavior.NoAction); // Avoid deleting payment on order deletion

            // Delivery
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Order)
                .WithOne()
                .HasForeignKey<Delivery>(d => d.OrderId)
                .OnDelete(DeleteBehavior.NoAction); // Avoid deleting order on delivery deletion

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.DeliveryPerson)
                .WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.DeliveryPersonId)
                .OnDelete(DeleteBehavior.SetNull); // Set null if delivery person is deleted

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne()
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction); // Avoid deleting order on payment deletion
            //Address
            modelBuilder.Entity<Address>()
                .HasMany(a => a.Orders)
                .WithOne(o => o.Address)
                .HasForeignKey(o => o.AddressId);
            // Seed Data
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" },
                new IdentityRole { Name = "DeliveryPerson", NormalizedName = "DELIVERYPERSON" }
            );
        }
    }
}
