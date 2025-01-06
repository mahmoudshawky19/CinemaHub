 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class FavoriteMovieRepository : Repository<FavoriteMovie>, IFavoriteMovieRepository
    {
     private readonly   ApplicationDbContext dbContext; 
        public FavoriteMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
