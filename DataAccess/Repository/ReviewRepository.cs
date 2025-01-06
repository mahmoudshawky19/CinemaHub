 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class ReviewRepository : Repository<MovieRating>, IReviewRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;

        }
    }
}
