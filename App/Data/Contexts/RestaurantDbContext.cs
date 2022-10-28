using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Entities;

namespace Restaurant.App.Data.Contexts;

public class RestaurantDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
    {}
}
