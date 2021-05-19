

using Microsoft.EntityFrameworkCore;

public class ProductsContext : DbContext
{

    public ProductsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Products> products { get; set; }

}