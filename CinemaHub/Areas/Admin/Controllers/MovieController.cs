 using CinemaHub.Repository;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CinemaHub.Controllers
{
    [Area("Admin")]

    public class MovieController : Controller
    {
        private readonly IMovieRepository movieRepository; private readonly ICinemaRepository cinemaRepository;
        private readonly ICategoryRepository categoryRepository ;
        public MovieController(IMovieRepository movieRepository, ICinemaRepository cinemaRepository , ICategoryRepository categoryRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository; this.categoryRepository = categoryRepository;

        }
public IActionResult Index(int page = 1)
{
    int pageSize = 5;  
    var totalMovies = movieRepository.GetAll().Count();  
    var totalPages = (int)Math.Ceiling((double)totalMovies / pageSize); 

    if (page <= 0) page = 1;  
    if (page > totalPages) page = totalPages;

            var movies = movieRepository.GetAll([e => e.Cinema, e => e.Category])
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;  
    ViewBag.CurrentPage = page;  

    return View(movies);
}
        public IActionResult Create()
        {
            ViewBag.MovieStatus = Enum.GetValues(typeof(MovieStatus)).Cast<MovieStatus>().ToList();

            ViewBag.c = cinemaRepository.GetAll( ).ToList();
            ViewBag.ce = categoryRepository.GetAll().ToList();

            return View( );
        }
        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile ImgUrl)
        {

            if (ImgUrl != null && ImgUrl.Length > 0)  
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);  
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                movie.ImgUrl = fileName;
            }

            movieRepository.Add(movie);
            movieRepository.Commit();

            TempData["success"] = "Add Movie Successfully";


            return RedirectToAction(nameof(Index));


         }
        public IActionResult Edit(int movieid)
        {
            ViewBag.c = cinemaRepository.GetAll().ToList(); ViewBag.ce = categoryRepository.GetAll().ToList();
            ViewBag.MovieStatus = Enum.GetValues(typeof(MovieStatus)).Cast<MovieStatus>().ToList();

            var cat = movieRepository.GetOne([ ]).FirstOrDefault(e => e.MovieId == movieid);
            return View(model: cat);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile ImgUrl)
        {
var oldProduct = movieRepository.GetOne(
    includeProp: null,
    expression: e => e.MovieId == movie.MovieId,
    tracked: false
).FirstOrDefault();
            if (oldProduct == null)
            {
                return RedirectToAction("");  
            }
            if (ImgUrl != null && ImgUrl.Length > 0)  
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);  
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", oldProduct.ImgUrl);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
                if (System.IO.File.Exists(oldfilePath))
                {
                    System.IO.File.Delete(oldfilePath);
                }
                movie.ImgUrl = fileName;
            }
            else
            {
                movie.ImgUrl = oldProduct.ImgUrl;
            }

            movieRepository.Edit(movie);
            movieRepository.Commit();
            TempData["success"] = "Edit Movie successfuly";
            return RedirectToAction("");

        }


        public IActionResult Delete(int movieid)
        {
            var c = movieRepository.GetOne([], e => e.MovieId == movieid).FirstOrDefault();
             movieRepository.Delete(c);
            movieRepository.Commit();
            TempData["success"] = "Delete Movie successfuly";

            return RedirectToAction("Index");

        }

    }
}
