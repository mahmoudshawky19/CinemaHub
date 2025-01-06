 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CinemaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;

        }
    }
}
