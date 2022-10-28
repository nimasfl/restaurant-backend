namespace Restaurant.App.Data.Models.Orders.Dto;

public class BaseOrderItemModel
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int TotalPrice { get; set; }
    public string FoodName { get; set; }
}
