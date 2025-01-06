namespace Models
{  public enum MovieStatus
        {
            Upcoming,
            Available,
            Expired
        }
    public class Movie
    {

        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImgUrl { get; set; }
        public string TrailerUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate
        {
            get; set;
        }
        public MovieStatus MovieStatus { get; set; }

        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<ActorMovie> Actors { get; set; }
        public bool IsFavorite { get; set; } = false;

        public List<MovieRating> Reviews { get; set; }
 

    }
}
