 
using CinemaHub.Repository;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace CinemaHub.Controllers
{
    [Area("Admin")]

    public class CinemaController : Controller
    {
       private readonly ICinemaRepository cinemaRepository;
        public CinemaController(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;  
            var totalCinemas = cinemaRepository.GetAll().Count(); 
            var totalPages = (int)Math.Ceiling((double)totalCinemas / pageSize);   

            if (page <= 0) page = 1;  
            if (page > totalPages) page = totalPages;

            var cinemas = cinemaRepository.GetAll([e=>e.Movies])
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(model: cinemas);
        }
        public IActionResult Create()
        {
            Cinema cinema = new Cinema();
            return View(cinema);
        }
       
       
        [HttpPost]
        public IActionResult Create(Cinema cinema, IFormFile CinemaLogo)
        {
            if (ModelState.IsValid)
            {
                if (CinemaLogo != null && CinemaLogo.Length > 0)  
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(CinemaLogo.FileName);  
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CinemaLogo", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        CinemaLogo.CopyTo(stream);
                    }

                    cinema.CinemaLogo = fileName;
                }

                cinemaRepository.Add(cinema);
                cinemaRepository.Commit();

                TempData["success"] = "Add Cinema Successfully";


                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); 
                }
                return View(cinema);
            }
            return View(cinema);
        }

        public IActionResult Edit(int cinemaId)
        {
       var cat = cinemaRepository.GetOne( ).FirstOrDefault(e => e.Id == cinemaId) ;
            return View(model:cat );
        }
        [HttpPost]
        public IActionResult Edit(Cinema cinema, IFormFile CinemaLogo)
        {
            var oldProduct = cinemaRepository.GetOne([],e => e.Id == cinema.Id,false  ).FirstOrDefault();
            
                if (oldProduct == null)
                {
                    return RedirectToAction("");  
                }
                if (CinemaLogo != null && CinemaLogo.Length > 0)  
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(CinemaLogo.FileName);  
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CinemaLogo", fileName);
                    var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CinemaLogo", oldProduct.CinemaLogo);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        CinemaLogo.CopyTo(stream);
                    }
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                    cinema.CinemaLogo = fileName;
                }
                else
                {
                    cinema.CinemaLogo = oldProduct.CinemaLogo;
                }

                cinemaRepository.Edit(cinema);
                cinemaRepository.Commit();
                TempData["success"] = "Edit Cinema successfuly";
                return RedirectToAction("");
       
 
            return View(cinema);
        }

        public IActionResult Delete(int cinemaid)
        {
            Cinema c = new Cinema() { Id = cinemaid };
            cinemaRepository.Delete(c);
            cinemaRepository.Commit();
            TempData["success"] = "Delete Cinema successfuly";

            return RedirectToAction("Index");

        }
    }
}
