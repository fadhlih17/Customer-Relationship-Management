using CustomerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(builder =>
        {
            builder.HasIndex(customer => customer.Email).IsUnique();
        });
    }
}