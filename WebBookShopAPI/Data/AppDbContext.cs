using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<SelectionOfBooks> SelectionOfBooks { get; set; }
    }
}
