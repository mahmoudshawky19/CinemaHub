namespace Models
{
    public class ActorMovie
    {
        public int ActorMovieId { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }


    }
}
