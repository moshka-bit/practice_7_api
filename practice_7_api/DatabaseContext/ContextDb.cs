using practice_7_api.Models;
using Microsoft.EntityFrameworkCore;


namespace practice_7_api.DatabaseContext

{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Author> Athores { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Rent_Book> Rent_Books { get; set; }

    }
}