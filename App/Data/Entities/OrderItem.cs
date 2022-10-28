namespace Restaurant.App.Data.Entities;

public class OrderItem: BaseEntity
{
    public Food Food { get; set; }
    public Guid FoodId { get; set; }
    public Order Order { get; set; }
    public Guid OrderId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int TotalPrice { get; set; }
}
