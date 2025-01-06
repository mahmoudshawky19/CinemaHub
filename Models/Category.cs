using System.ComponentModel.DataAnnotations;

namespace  Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "mininum Length must be 3")]
        [MaxLength(50, ErrorMessage = "maxinum Length must be 50")]
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
