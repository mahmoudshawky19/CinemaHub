 
using CinemaHub.Repository;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CinemaHub.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;
        ICinemaRepository cinemaRepository; 
        IMovieRepository movieRepository;
        public CategoryController(ICategoryRepository categoryRepository , ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
        {
            this.categoryRepository = categoryRepository;
            this.cinemaRepository = cinemaRepository;   
            this.movieRepository = movieRepository;
        }
         public IActionResult Index(int page = 1 )
        {
            int pageSize = 5;
            var totalMovies = categoryRepository.GetAll().Count();
            var totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

            if (page <= 0) page = 1;
            if (page > totalPages) page = totalPages;

            var c = categoryRepository.GetAll( )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;


             return View(model: c);
        }
        public IActionResult Cinemas()
        {
            var c = cinemaRepository.GetAll().ToList();
            return View(model: c);
        }
        public IActionResult Kinds(string name)
        {
            var r = movieRepository.GetAll([e=>e.Category , e=>e.Cinema] , e => e.Category.Name == name || e.Cinema.Name == name  ).ToList() ;
            return View(model: r);
        }
        public IActionResult Index_Two(int page = 1)
        {
            int pageSize = 5;  
            var totalCategories = categoryRepository.GetAll().Count(); 
            var totalPages = (int)Math.Ceiling((double)totalCategories / pageSize); 

            if (page <= 0) page = 1;  
            if (page > totalPages) page = totalPages;  

            var categories = categoryRepository.GetAll([e => e.Movies])
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(model: categories);
        }
        public IActionResult Create()
        {
          Category category = new Category();
            return View(category);
        }
        [HttpPost]
         public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                categoryRepository.Commit();
                TempData["success"] = "Add Category Successfully";
                return RedirectToAction("Index_Two");
            }
            return View(category);
         }
        public IActionResult Edit(int categoryid)
        {
            var cat = categoryRepository.GetOne([]).FirstOrDefault(e => e.Id == categoryid);
            return View(model: cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Edit(category);
                categoryRepository.Commit();
                TempData["success"] = "Edit Category Successfully";
                return RedirectToAction("Index_Two");
            }
            return View(category);
        }


        public IActionResult Delete(int categoryid)
        {
            Category c = new Category() { Id = categoryid };
            categoryRepository.Delete(c);
            categoryRepository.Commit(); TempData["success"] = "Delete Category Successfully";

            return RedirectToAction(nameof(Index_Two));

        }

    }
}
