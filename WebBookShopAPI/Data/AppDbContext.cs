using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default Values
            {
                modelBuilder.Entity<Author>()
                     .Property(b => b.UploadedInfo)
                     .ValueGeneratedOnAdd()
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Author>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Genre>()
                     .Property(b => b.UploadedInfo)
                     .ValueGeneratedOnAdd()
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Genre>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<BookSeries>()
                     .Property(b => b.UploadedInfo)
                     .ValueGeneratedOnAdd()
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<BookSeries>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<SelectionOfBooks>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<SelectionOfBooks>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Book>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Book>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Book>()
                     .Property(b => b.Rating)
                     .HasDefaultValueSql("0");

                modelBuilder.Entity<Publisher>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Publisher>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Order>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Order>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Delivery>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Delivery>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<OrderStatus>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<OrderStatus>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Order>()
                     .Property(b => b.OrderStatusId)
                     .HasDefaultValueSql("1");

                modelBuilder.Entity<Order>()
                    .Property(b => b.Address)
                    .HasDefaultValue("проспект Людвіга Свободи, 33, Харків, Харківська область, 61000");

                modelBuilder.Entity<Review>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Review>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            }

            // Fields
            {
                modelBuilder.Entity<Book>(entity =>
                {
                    entity.Property(e => e.Price)
                        .HasColumnType("decimal(18,2)");
                });

                modelBuilder.Entity<Order>(entity =>
                {
                    entity.Property(e => e.Sum)
                        .HasColumnType("decimal(18,2)");
                });

                modelBuilder.Entity<Delivery>(entity =>
                {
                    entity.Property(e => e.Price)
                        .HasColumnType("decimal(18,2)");
                });

            }

            // OrderItem
            {
                modelBuilder.Entity<OrderItem>().HasKey(am => new
                {
                    am.BookId,
                    am.OrderId
                });

                modelBuilder.Entity<OrderItem>(entity =>
                {
                    entity.Property(e => e.Price)
                        .HasColumnType("decimal(18,2)");
                });

                modelBuilder.Entity<OrderItem>().HasOne(m => m.Book).WithMany(am => am.OrderItem).HasForeignKey(m =>
                m.BookId);

                modelBuilder.Entity<OrderItem>().HasOne(m => m.Order).WithMany(am => am.OrderItem).HasForeignKey(m =>
                m.OrderId);
            }

            // User Selected Book
            {
                modelBuilder.Entity<UserSelectedBook>().HasKey(am => new
                {
                    am.BookId,
                    am.AppUserId
                });

                modelBuilder.Entity<UserSelectedBook>().HasOne(m => m.Book).WithMany(am => am.UserSelectedBook).HasForeignKey(m =>
                m.BookId);

                modelBuilder.Entity<UserSelectedBook>().HasOne(m => m.AppUser).WithMany(am => am.UserSelectedBook).HasForeignKey(m =>
                m.AppUserId);
            }

            // Gender For User
            {
                modelBuilder.Entity<Gender>()
                   .HasKey(g => g.GenderCode);

                modelBuilder.Entity<AppUser>()
                    .HasOne(a => a.Gender)
                    .WithMany()
                    .HasForeignKey(a => a.UserGenderCode);
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<SelectionOfBooks> SelectionOfBooks { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; } 
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<UserSelectedBook> UserSelectedBook { get; set; }

    }
}
