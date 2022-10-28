using System.ComponentModel.DataAnnotations;

namespace Restaurant.App.Data.Entities;

public class Food: BaseEntity
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Type { get; set; }
    public string ImageUrl { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
