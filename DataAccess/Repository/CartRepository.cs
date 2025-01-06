 
using DataAccess;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CinemaHub.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext dbContext;
        DbSet<Movie> dbSet;
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<Movie>();
        }

    }
}
