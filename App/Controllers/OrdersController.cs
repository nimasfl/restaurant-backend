using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.App.Data.Models.Orders.Dto;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Services;

namespace Restaurant.App.Controllers;

[Route("[controller]")]
[ApiController, Authorize]
public class OrdersController: ControllerBase
{
    private readonly OrdersService _ordersService;

    public OrdersController(OrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    public async Task<List<BaseOrderModel>> GetOrders(Requester requester)
    {
        return await _ordersService.GetOrders(requester.Id);
    }

    [HttpPost]
    public async Task CreateOrder([FromBody]CreateOrderRequestDto dto ,Requester requester)
    {
        await _ordersService.CreateOrder(dto,requester.Id);
    } 
}

