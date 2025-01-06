using CinemaHub.Repository;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CinemaHallController : Controller
    {
        private readonly ICinemaHallReposirory cinemaHallReposirory;
        private readonly ICinemaRepository cinemaRepository;
        public CinemaHallController(ICinemaHallReposirory cinemaHallReposirory, ICinemaRepository cinemaRepository)
        {
            this.cinemaHallReposirory = cinemaHallReposirory;
            this.cinemaRepository = cinemaRepository;
        }
        public IActionResult Index(int page=1)
        {
            int pageSize = 5;

            var totalActors = cinemaHallReposirory.GetAll().Count();
            var totalPages = (int)Math.Ceiling((double)totalActors / pageSize);

            if (page <= 0) page = 1;
            if (page > totalPages) page = totalPages;

            var cinemas = cinemaHallReposirory.GetAll([e => e.Cinema])
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

 
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
 
            var chs =    cinemaHallReposirory.GetAll([e => e.Cinema]).ToList();
            return View(chs);
        }
       public IActionResult Create()
        {
            ViewBag.Cinemas = cinemaRepository.GetAll().ToList();

            return View( );
        }
        [HttpPost]
         public IActionResult Create(CinemaHall cinemaHall)
        {
            if (ModelState.IsValid)
            {
                cinemaHallReposirory.Add(cinemaHall);
                cinemaHallReposirory.Commit();
                TempData["success"] = "Add cinemaHall Successfully";
                return RedirectToAction("Index");
            }
            return View(cinemaHall);
         }
        public IActionResult Edit(int cinemaHallid)
        {
            ViewBag.Cinemas = cinemaRepository.GetAll().ToList();
            var cat = cinemaHallReposirory.GetOne([]).FirstOrDefault(e => e.Id == cinemaHallid);
            return View(model: cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CinemaHall cinemaHall)
        {
            if (ModelState.IsValid)
            {
                cinemaHallReposirory.Edit(cinemaHall);
                cinemaHallReposirory.Commit();
                TempData["success"] = "Edit CinemaHall Successfully";
                return RedirectToAction("Index");
            }
            return View(cinemaHall);
        }


        public IActionResult Delete(int cinemaHallid)
        {
            CinemaHall c = new CinemaHall() { Id = cinemaHallid };
            cinemaHallReposirory.Delete(c);
            cinemaHallReposirory.Commit();
            TempData["success"] = "Delete CinemaHall Successfully";

            return RedirectToAction(nameof(Index));

        }
  
    }
}
