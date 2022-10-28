namespace Restaurant.App.Data.Entities;

public class CartItem: BaseEntity
{
    public Food Food { get; set; }
    public Guid FoodId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public int Count { get; set; }
}
