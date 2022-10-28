namespace Restaurant.App.Data.Models.Orders.Dto;

public class BaseOrderModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string Address { get; set; }
    public int TotalPrice { get; set; }
    public List<BaseOrderItemModel> Items { get; set; } = new();
}
