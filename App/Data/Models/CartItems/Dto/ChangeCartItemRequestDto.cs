namespace Restaurant.App.Data.Models.CartItems.Dto;

public class ChangeCartItemRequestDto
{
    public Guid FoodId { get; set; }
    public int Count { get; set; }
}
