using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        public string Name { get; set; }
        public int? Rating { get; set; }
    public string? Review { get; set; }
}
}
