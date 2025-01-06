using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class _ٌReviewController : Controller
    {
        private readonly IReviewRepository reviewRepository;
        public _ٌReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public IActionResult Index()
        {
           var reviews =  reviewRepository.GetAll( ).ToList();
            return View(reviews);
        }
        public IActionResult Delete(int reviewId)
        {
            var review = reviewRepository.GetOne([], e => e.Id == reviewId).FirstOrDefault();
            reviewRepository.Delete(review);
            reviewRepository.Commit();
            TempData["Success"] = "Delete Review Successfully"; 
            return RedirectToAction("Index");   
        }
    }
}
