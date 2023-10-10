using BookLib;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookLib
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
