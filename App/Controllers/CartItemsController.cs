using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.App.Data.Models.CartItems.Dto;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Services;

namespace Restaurant.App.Controllers;

[Route("[controller]")]
[ApiController, Authorize]
public class CartItemsController: ControllerBase
{
    private readonly CartItemsService _cartItemsService;

    public CartItemsController(CartItemsService cartItemsService)
    {
        _cartItemsService = cartItemsService;
    }

    [HttpPost("[action]")]
    public async Task<ChangeCartItemResponseDto> ChangeCartItem([FromBody] ChangeCartItemRequestDto dto, Requester requester)
    {
        return await _cartItemsService.ChangeCartItem(dto, requester.Id);
    }

    [HttpGet]
    public async Task<List<BaseCartItemModel>> GetCartItems(Requester requester)
    {
        return await _cartItemsService.GetCartItems(requester.Id);
    }
}
