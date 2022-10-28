using System.ComponentModel.DataAnnotations;

namespace Restaurant.App.Data.Entities;

public class User: BaseEntity
{
    [Required] public string Username { get; set; }
    [Required] public string HashedPassword { get; set; }
    [Required] public string Name { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<Order> Orders { get; set; }
}
