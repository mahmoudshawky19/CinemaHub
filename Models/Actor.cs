namespace Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }

        public List<ActorMovie> Movies { get; set; }

        public string News { get; set; }


    }
}
