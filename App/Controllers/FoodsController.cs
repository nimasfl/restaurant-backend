using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Entities;
using Restaurant.App.Data.Models.Foods.Dto;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Services;

namespace Restaurant.App.Controllers;

[Route("[controller]")]
[ApiController, Authorize]
public class FoodsController : ControllerBase
{
    private readonly FoodsService _foodsService;
    private readonly RestaurantDbContext _dbContext;

    public FoodsController(FoodsService foodsService, RestaurantDbContext dbContext)
    {
        _foodsService = foodsService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<BaseFoodModel>> GetFoods([FromQuery] GetFoodsRequestDto dto, Requester requester)
    {
        return await _foodsService.GetFoods(dto, requester.Id);
    }

    [HttpGet("[action]")]
    public async Task<List<string>> GetFoodTypes()
    {
        return await _foodsService.GetFoodTypes();
    }
}
