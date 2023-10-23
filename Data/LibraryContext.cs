using Microsoft.EntityFrameworkCore;
using Moscu_Diana_Stephani_Lab2.Models;

namespace Moscu_Diana_Stephani_Lab2.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) :
        base(options)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Customer>().ToTable("Customer"); 
                modelBuilder.Entity<Order>().ToTable("Order"); 
                modelBuilder.Entity<Book>().ToTable("Book");
            }
    }
}
