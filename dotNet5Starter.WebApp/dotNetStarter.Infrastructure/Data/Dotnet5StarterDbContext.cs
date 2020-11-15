using dotNet5Starter.Infrastructure.Data.DataModels;
using dotNet5Starter.Webapp.Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore; 

namespace dotNet5Starter.Webapp.Infrastructure
{
    public class Dotnet5StarterDbContext : DbContext
    {
        public Dotnet5StarterDbContext(DbContextOptions<Dotnet5StarterDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
