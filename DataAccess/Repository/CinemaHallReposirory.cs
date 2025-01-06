 
using DataAccess;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class CinemaHallReposirory : Repository<CinemaHall>, ICinemaHallReposirory
    {
     private readonly   ApplicationDbContext dbContext; 
        public CinemaHallReposirory(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
