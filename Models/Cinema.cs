using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "mininum Length must be 3")]
        [MaxLength(50, ErrorMessage = "maxinum Length must be 50")]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string Address { get; set; }
        [ValidateNever]


        public string CinemaLogo { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<CinemaHall> cinemaHalls { get; set; } = new List<CinemaHall>();

 



    }
}
