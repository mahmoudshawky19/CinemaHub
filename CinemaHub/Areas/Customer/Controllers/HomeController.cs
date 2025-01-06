 
using CinemaHub.Repository;
using DataAccess.IRepository;
using DataAccess.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
 
namespace CinemaHub.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
         private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository movieRepository;
        private readonly IFavoriteMovieRepository favoriteMovieRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IActorMovieRepository actorMovieRepository;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(IReviewRepository reviewRepository,IFavoriteMovieRepository favoriteMovieRepository,UserManager<IdentityUser> userManager,ILogger<HomeController> logger , IMovieRepository movieRepository , IActorMovieRepository actorMovieRepository)
        {
            this.userManager = userManager;

            _logger = logger;
            this.movieRepository = movieRepository;
            this.reviewRepository = reviewRepository;
            this.favoriteMovieRepository = favoriteMovieRepository;
            this.actorMovieRepository = actorMovieRepository;
        }

        public IActionResult Index()
        {

            var c = movieRepository.GetAll([e => e.Cinema, e => e.Category]);
            return View(model: c.ToList());
        }
        [HttpGet]
        public IActionResult AddToFavorites(int movieid)
        {
            var user = userManager.GetUserId(User);
            if (user != null)
            {
                var movie = movieRepository.GetOne([], e => e.MovieId == movieid).FirstOrDefault();
                FavoriteMovie favoriteMovie = new FavoriteMovie()
                {
                    MovieId = movieid,
                    ApplicationUserId = userManager.GetUserId(User)

                };
                if (movie.IsFavorite == false)
                {
                    movie.IsFavorite = true;
                    favoriteMovieRepository.Add(favoriteMovie);
                    favoriteMovieRepository.Commit();
                    TempData["Success"] = "Add to favorite successfully";
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                else
                {
                    movie.IsFavorite = false;
                    var favmovieDeleted = favoriteMovieRepository.GetOne([], e => e.MovieId == movie.MovieId).FirstOrDefault();
                    favoriteMovieRepository.Delete(favmovieDeleted);
                    favoriteMovieRepository.Commit();
                    TempData["Success"] = "Movie removed from favorites.";
                    return RedirectToAction("Index", "Home", new { area = "Customer" });

                }
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
        }
        public IActionResult FAV()
        {
          var movies =   movieRepository.GetAll([e => e.Cinema, e => e.Category], e=>e.IsFavorite ==true).ToList();
            return View(movies); 
        }
        public IActionResult Details(int movieid)
        {
            ViewBag.Reviews = reviewRepository.GetAll().ToList();
            ViewBag.c = actorMovieRepository.GetAll( [e=>e.Actor] ,e => e.MovieId == movieid).Select(e => e.Actor).ToList() ;    
            var movie  = movieRepository.GetOne([e => e.Cinema, e => e.Category,e=>e.Reviews ], e => e.MovieId == movieid) ;
            return View(model: movie);
        }
        [HttpPost]
        public IActionResult SubmitReview(MovieRating movieRating)
        {
            reviewRepository.Add(movieRating);
            reviewRepository.Commit();
            TempData["ReviewSuccess"] = "Thank you for your review! We appreciate your feedback.";
            return RedirectToAction("Details", "Home", new { area = "Customer", movieId = movieRating.MovieId });

        }

        [HttpGet]
        public IActionResult Search(string word)
        {
            var results = movieRepository.GetAll([e => e.Category, e => e.Cinema], e => e.Name.Contains(word));
             return View(results);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
