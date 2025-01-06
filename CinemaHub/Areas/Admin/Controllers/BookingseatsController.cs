using CinemaHub.Repository;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BookingseatsController : Controller
    {
        private readonly IBookingseatsRepository bookingseatsRepository;
        private readonly ICinemaHallReposirory cinemaHallReposirory;
        private readonly ICinemaRepository cinemaReposirory;
        private readonly IMovieRepository movieRepository;
        public BookingseatsController(IMovieRepository movieRepository,IBookingseatsRepository bookingseatsRepository , ICinemaHallReposirory cinemaHallReposirory , ICinemaRepository cinemaReposirory)
        {
            this.cinemaReposirory = cinemaReposirory;
            this.cinemaHallReposirory = cinemaHallReposirory;
            this.bookingseatsRepository = bookingseatsRepository;
            this.movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
         var books = bookingseatsRepository.GetAll([e => e.Movie , e=>e.Cinema,e=>e.CinemaHall]).ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.CinemaHalls = cinemaHallReposirory.GetAll().ToList();
            ViewBag.Cinemas = cinemaReposirory.GetAll().ToList();
            ViewBag.Movies = movieRepository.GetAll().ToList();
            Bookingseats bookingseats = new Bookingseats();
            return View(bookingseats);
        }
        [HttpPost]
        public IActionResult Create(Bookingseats bookingseats)
        {
            if (ModelState.IsValid)
            {
                bookingseatsRepository.Add(bookingseats);
                bookingseatsRepository.Commit();
                TempData["success"] = "Add bookingseats Successfully";
                return RedirectToAction("Index");
            }
            return View(bookingseats);
        }
        public IActionResult Edit(int bookingseatid)
        {
            ViewBag.CinemaHalls = cinemaHallReposirory.GetAll().ToList();
            ViewBag.Cinemas = cinemaReposirory.GetAll().ToList();
            ViewBag.Movies = movieRepository.GetAll().ToList();
            var cat = bookingseatsRepository.GetOne([] ,e => e.Id == bookingseatid).FirstOrDefault();
            return View(model: cat);
        }
        [HttpPost]
         public IActionResult Edit(Bookingseats bookingseats)
        {
            if (ModelState.IsValid)
            {
                bookingseatsRepository.Edit(bookingseats);
                bookingseatsRepository.Commit();
                TempData["success"] = "Edit bookingseats Successfully";
                return RedirectToAction("Index");
            }
            return View(bookingseats);
        }


        public IActionResult Delete(int bookingseatid)
        {
            var book = bookingseatsRepository.GetOne([],e=>e.Id==bookingseatid).FirstOrDefault();
             bookingseatsRepository.Delete(book);
            bookingseatsRepository.Commit();
            TempData["success"] = "Delete bookingseats Successfully";

            return RedirectToAction(nameof(Index));

        }


    }
}
