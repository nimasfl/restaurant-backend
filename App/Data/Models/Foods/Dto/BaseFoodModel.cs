namespace Restaurant.App.Data.Models.Foods.Dto;

public class BaseFoodModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Type { get; set; }
    public string ImageUrl { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public int Count { get; set; }
}
