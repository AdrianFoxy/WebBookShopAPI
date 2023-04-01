using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data
{
    public class AppDbContext : DbContext
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
                     .ValueGeneratedOnAddOrUpdate()
                     .HasDefaultValueSql("0");

                modelBuilder.Entity<Publisher>()
                    .Property(b => b.UploadedInfo)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity<Publisher>()
                    .Property(b => b.UpdatedInfo)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<SelectionOfBooks> SelectionOfBooks { get; set; }
    }
}
