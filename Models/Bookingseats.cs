using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class Bookingseats
    {
        [ValidateNever]  
        public CinemaHall CinemaHall { get; set; }

        [ValidateNever] 
        public Cinema Cinema { get; set; }

        [ValidateNever] 
        public Movie Movie { get; set; }

        public int CinemaHallId { get; set; }
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public DateTime DateTime { get; set; }

        public int Id { get; set; }
        public int? from { get; set; }
        public int? to { get; set; }
        public int? numberseat { get; set; }
    }
}
