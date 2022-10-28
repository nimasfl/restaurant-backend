namespace Restaurant.App.Data.Models.Foods.Dto;

public class CreateFoodRequestDto
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Type { get; set; }
    public string ImageUrl { get; set; }
}
