 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class ActorMovieRepository : Repository<ActorMovie>, IActorMovieRepository
    {
        private readonly ApplicationDbContext dbContext; 
        public ActorMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;

        }
    }
}
