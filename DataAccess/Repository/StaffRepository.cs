 
using DataAccess;
using DataAccess.IRepository;
using Models;

namespace CinemaHub.Repository
{
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
     private readonly   ApplicationDbContext dbContext; 
        public StaffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
