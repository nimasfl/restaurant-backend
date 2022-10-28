using Microsoft.EntityFrameworkCore;
using Restaurant.App.Data.Contexts;

namespace Restaurant.App.Repository
{
    public class Repository<TEntity>
        where TEntity : class
    {
        public RestaurantDbContext _dbContext;
        protected DbSet<TEntity> Entities { get; }
        protected virtual IQueryable<TEntity> Table => Entities.AsNoTracking();
        public Repository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
        }
    }
}
