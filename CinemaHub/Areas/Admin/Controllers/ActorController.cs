 
using CinemaHub.Repository;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CinemaHub.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
       private readonly IActorRepository actorRepository;
        private readonly IActorMovieRepository actorMovieRepository;
        private readonly IMovieRepository movieRepository;
        public ActorController(IActorRepository actorRepository , IActorMovieRepository actorMovieRepository, IMovieRepository movieRepository)
        {
            this.actorRepository = actorRepository;this.actorMovieRepository = actorMovieRepository;
            this.movieRepository = movieRepository; 
        }


        public IActionResult Index(string search = null, int page = 1)
        {
            int pageSize = 5;
 
            var totalActors = actorRepository.GetAll().Count();
            var totalPages = (int)Math.Ceiling((double)totalActors / pageSize);

            if (page <= 0) page = 1;
            if (page > totalPages) page = totalPages;

            var actors = actorRepository.GetAll([e=>e.Movies])
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var moviesCount = actors.ToDictionary(
                actor => actor.ActorId,
                actor => actorMovieRepository.GetAll([],e => e.ActorId == actor.ActorId).Count()
            );

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.MoviesCount = moviesCount; 

            return View(model: actors);
        }
        public IActionResult Create()
        {
            ViewBag.a = movieRepository.GetAll().ToList();

            return View(   );
        }


        [HttpPost]
        public IActionResult Create(Actor actor, IFormFile ProfilePicture, List<int> MovieId)
        {
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                actor.ProfilePicture = fileName;
            }

            actorRepository.Add(actor);
            actorRepository.Commit();

             if (MovieId != null && MovieId.Any())  
            {
                foreach (var movieId in MovieId)
                {
                    actorMovieRepository.Add(new ActorMovie{ActorId = actor.ActorId, MovieId = movieId });
                }
                actorMovieRepository.Commit();
            }

            TempData["success"] = "Add Actor Successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int actorid)
        {
            ViewBag.a = movieRepository.GetAll();  
            var actor = actorRepository.GetOne(null, e => e.ActorId == actorid).FirstOrDefault();

            if (actor == null)
            {
                return NotFound();  
            }

             var currentMovieIds = actorMovieRepository.GetAll(null, e => e.ActorId == actor.ActorId)
                                    .Select(am => am.MovieId).ToList();

            ViewBag.CurrentMovieIds = currentMovieIds; 
 
            return View(model: actor); 
         }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile ProfilePicture, List<int> MovieId)
        {
            var oldActor = actorRepository.GetOne(null, e => e.ActorId == actor.ActorId,false).FirstOrDefault();
            if (oldActor == null)
            {
                return NotFound();
            }

             if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", oldActor.ProfilePicture);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                 if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                actor.ProfilePicture = fileName;
            }
            else
            {
                actor.ProfilePicture = oldActor.ProfilePicture;  
            }

             actorRepository.Edit(actor);
            actorRepository.Commit();

             var existingActorMovies = actorMovieRepository.GetAll([], e => e.ActorId == actor.ActorId).ToList();
            foreach (var existing in existingActorMovies)
            {
                actorMovieRepository.Delete(existing);
            }
            actorMovieRepository.Commit();

             if (MovieId != null && MovieId.Any())
            {
                foreach (var movieId in MovieId)
                {
                    actorMovieRepository.Add(new ActorMovie
                    {
                        ActorId = actor.ActorId,
                        MovieId = movieId
                    });
                }
                actorMovieRepository.Commit();
            }

            TempData["success"] = "Edit Actor successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int actorid)
        {
            
            Actor c = actorRepository.GetOne([],e=>e.ActorId==actorid).FirstOrDefault();
            actorRepository .Delete(c);
            actorRepository.Commit();  
            TempData["success"] = "Delete Actor Successfully";

            return RedirectToAction(nameof(Index));

        }


    }
}
 