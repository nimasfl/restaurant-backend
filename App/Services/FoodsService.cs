using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Models.Foods.Dto;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.App.Services;

[Injectable]
public class FoodsService
{
    private readonly RestaurantDbContext _dbContext;

    public FoodsService(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BaseFoodModel>> GetFoods(GetFoodsRequestDto dto, Guid requesterId)
    {
        var query = _dbContext.Foods
            .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(dto.Type))
        {
            query = query.Where(f => f.Type == dto.Type);
        }

        return await query
            .OrderByDescending(f => f.CreatedOn)
            .Select(f => new BaseFoodModel
            {
                Id = f.Id,
                Price = f.Price,
                Type = f.Type,
                Name = f.Name,
                CreatedOn = f.CreatedOn,
                ImageUrl = f.ImageUrl,
                Count = f.CartItems.Any(c => c.UserId == requesterId)
                    ? f.CartItems.SingleOrDefault(c => c.UserId == requesterId)!.Count
                    : 0
            })
            .ToListAsync();
    }

    public async Task<List<string>> GetFoodTypes()
    {
        return await _dbContext.Foods
            .GroupBy(f => f.Type, (g, list) => g)
            .ToListAsync();
    }
}
