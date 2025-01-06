using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class CinemaHall
    {

        public int Id { get; set; }
        public int number { get; set; }
        [ValidateNever]

        public Cinema Cinema { get; set; }

        public int CinemaId {  get; set; }
 
        public string seats {  get; set; }
    }
}
