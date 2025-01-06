using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaHub.Repository;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class BookingseatsRepository : Repository<Bookingseats>, IBookingseatsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BookingseatsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
