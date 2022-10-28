using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Entities;
using Restaurant.App.Data.Models.CartItems.Dto;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.App.Services;

[Injectable]
public class CartItemsService
{
    private readonly RestaurantDbContext _dbContext;

    public CartItemsService(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChangeCartItemResponseDto> ChangeCartItem(ChangeCartItemRequestDto dto, Guid requesterId)
    {
        var cartItem = _dbContext.CartItems
            .SingleOrDefault(c => c.FoodId == dto.FoodId && c.UserId == requesterId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                FoodId = dto.FoodId,
                UserId = requesterId,
                CreatedOn = DateTime.UtcNow,
                Count = dto.Count,
            };
            _dbContext.CartItems.Add(cartItem);
        }
        else
        {
            if (dto.Count > 0)
            {
                cartItem.Count = dto.Count;
            }
            else
            {
                _dbContext.CartItems.Remove(cartItem);
            }
        }

        await _dbContext.SaveChangesAsync();
        return new ChangeCartItemResponseDto
        {
            Count = cartItem.Count,
            FoodId = cartItem.FoodId,
        };
    }

    public async Task<List<BaseCartItemModel>> GetCartItems(Guid requesterId)
    {
        return await _dbContext.CartItems
            .Where(c => c.UserId == requesterId)
            .Select(c => new BaseCartItemModel
            {
                Id = c.Id,
                FoodName = c.Food.Name,
                FoodId = c.FoodId,
                Price = c.Food.Price,
                Count = c.Count
            })
            .ToListAsync();
    }
}
