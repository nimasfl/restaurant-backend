namespace Restaurant.App.Data.Entities;

public class Order: BaseEntity
{
    public string Address { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public int TotalPrice { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
