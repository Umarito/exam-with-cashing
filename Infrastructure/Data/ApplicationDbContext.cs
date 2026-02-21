using Npgsql;
using Microsoft.EntityFrameworkCore;
public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}