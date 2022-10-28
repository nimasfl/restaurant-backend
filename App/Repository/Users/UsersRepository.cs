using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Contexts;
using Restaurant.App.Data.Entities;
using Restaurant.App.Data.Models.Requester;
using Restaurant.App.Data.Models.Users.Dto;
using Restaurant.Common.IoC.Attributes;

namespace Restaurant.App.Repository.Users;

[Injectable]
public class UsersRepository : Repository<User>
{
    public UsersRepository(RestaurantDbContext dbContext) : base(dbContext)
    {
    }
    

}
