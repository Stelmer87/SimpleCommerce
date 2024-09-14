using Microsoft.EntityFrameworkCore;
using SimpleCommerce.Infrastructure.Entities;
using System.Collections.Generic;

namespace SimpleCommerce.Infrastructure;

public class DataContext : DbContext
{
    internal DbSet<ProductEntity> Products { get; set; }
    internal DbSet<CartEntity> Carts { get; set; }

    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

}