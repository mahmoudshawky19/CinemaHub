 
using DataAccess;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace CinemaHub.Repository
{
    public class MovieRepository : Repository <Movie> , IMovieRepository
    {
        private readonly ApplicationDbContext dbContext;
        DbSet<Movie> dbSet;
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<Movie>();
        }
 

    }
}
