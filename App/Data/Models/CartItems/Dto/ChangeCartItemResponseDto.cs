namespace Restaurant.App.Data.Models.CartItems.Dto;

public class ChangeCartItemResponseDto
{
    public Guid FoodId { get; set; }
    public int Count { get; set; }
}
