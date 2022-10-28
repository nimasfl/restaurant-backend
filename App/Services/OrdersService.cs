using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Entities;
using Restaurant.App.Data.Models.Orders.Dto;
using Restaurant.App.Data.Models.Requester;
using Restaurant.Common.Exceptions.Responses;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.App.Services;

[Injectable]
public class OrdersService
{
    private readonly RestaurantDbContext _dbContext;

    public OrdersService(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOrder(CreateOrderRequestDto dto, Guid requesterId)
    {
        var cartItems = await _dbContext.CartItems
            .Where(c => c.UserId == requesterId)
            .Select(c => new
            {
                c.Id,
                c.Count,
                c.FoodId,
                c.Food.Price
            })
            .ToListAsync();
        if (!cartItems.Any() )
        {
            throw new BadRequestException("your cart is empty.");
        }
        var order = new Order
        {
            Address = dto.Address,
            UserId = requesterId,
            CreatedOn = DateTime.UtcNow,
        };
        _dbContext.Orders.Add(order);
        var orderItems = cartItems.Select(c => new OrderItem
            {
                Count = c.Count,
                Price = c.Price,
                TotalPrice = c.Price * c.Count,
                FoodId = c.FoodId,
                OrderId = order.Id,
                CreatedOn = DateTime.UtcNow,
            })
            .ToList();
        order.TotalPrice = orderItems.Select(c => c.TotalPrice).Sum();
        _dbContext.OrderItems.AddRange(orderItems);
        _dbContext.CartItems.RemoveRange(cartItems.Select(c => new CartItem {Id = c.Id}));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<BaseOrderModel>> GetOrders(Guid requesterId)
    {
        return await _dbContext
            .Orders.AsNoTracking()
            .Where(o => o.UserId == requesterId)
            .Select(o => new BaseOrderModel
            {
                Id = o.Id,
                Address = o.Address,
                CreatedOn = o.CreatedOn,
                TotalPrice = o.TotalPrice,
                Items = o.OrderItems.Select(oi => new BaseOrderItemModel
                    {
                        Id = oi.Id,
                        FoodName = oi.Food.Name,
                        Count = oi.Count,
                        Price = oi.Price,
                        TotalPrice = oi.TotalPrice,
                    })
                    .ToList()
            })
            .ToListAsync();
    }
}
