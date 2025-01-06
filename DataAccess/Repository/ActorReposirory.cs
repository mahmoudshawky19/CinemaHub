 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class ActorReposirory : Repository<Actor>, IActorRepository
    {
     private readonly   ApplicationDbContext dbContext; 
        public ActorReposirory(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
