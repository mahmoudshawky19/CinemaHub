using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<Cart> carts { get; set; }
        public DbSet<Bookingseats> bookingseats { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<CinemaHall> cinemaHalls { get; set; }
        public DbSet<FavoriteMovie> favoriteMovies { get; set; }
        public DbSet<MovieRating> movieRatings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Bookingseats>()
                .HasOne(b => b.Movie)
                .WithMany()
                .HasForeignKey(b => b.MovieId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Bookingseats>()
                .HasOne(b => b.Cinema)
                .WithMany()
                .HasForeignKey(b => b.CinemaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Bookingseats>()
                .HasOne(b => b.CinemaHall)
                .WithMany()
                .HasForeignKey(b => b.CinemaHallId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IdentityUser>()
                .Property(u => u.EmailConfirmed)
                .HasDefaultValue(true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
