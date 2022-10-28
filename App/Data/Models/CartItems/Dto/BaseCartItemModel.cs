namespace Restaurant.App.Data.Models.CartItems.Dto;

public class BaseCartItemModel
{
    public Guid Id { get; set; }
    public Guid FoodId { get; set; }
    public string FoodName { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public string ImageUrl { get; set; }
}
