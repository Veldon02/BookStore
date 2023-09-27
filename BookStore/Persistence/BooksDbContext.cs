using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey("AuthorId");

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Genre)
                        .WithMany(g => g.Books)
                        .HasForeignKey("GenreId");
        }
    }
}
