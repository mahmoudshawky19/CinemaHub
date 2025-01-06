 using CinemaHub.Repository;
using DataAccess.IRepository;
 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace CinemaHub.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepositery;
        private readonly UserManager<IdentityUser> userManager;
        public CartController(ICartRepository cartRepositery, UserManager<IdentityUser> userManager)
        {
            this.cartRepositery = cartRepositery;
            this.userManager = userManager;
        }
        public IActionResult AddToCart(int movieId , int count)
    {
            var user = userManager.GetUserId(User); 
            if(user ==null)
            {
                return RedirectToAction("Account", "Login", new { area = "Identity" });
            }
            Cart cart = new Cart()
            {
                count = count,
                MovieId = movieId,
                ApplicationUserId = userManager.GetUserId(User)
            };
            cartRepositery.Add(cart);
            cartRepositery.Commit(); 
            TempData["success"] = "Add movie to cart successfully";

            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }
        public IActionResult Index()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var c = cartRepositery.GetAll([e=>e.Movie] , e=>e.ApplicationUserId== ApplicationUserId).ToList();
            ViewBag.Total = c.Sum(e => e.Movie.Price * e.count);
            return View(c);
        }
        public IActionResult Increment(int movieId)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var movie = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MovieId == movieId).FirstOrDefault();

            if (movie != null)
            {
                    movie.count++;
                            cartRepositery.Commit();

                return RedirectToAction("Index");
            }

            return RedirectToAction("NotFoundPage", "Home");
        }
        public IActionResult Decrement(int movieId)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var movie = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MovieId == movieId).FirstOrDefault();

            if (movie != null)
            {
                movie.count--;
                if (movie.count > 0)

                    cartRepositery.Commit();

                else
                    movie.count = 1;

                return RedirectToAction("Index");
            }

            return RedirectToAction("NotFoundPage", "Home");
        }
        public IActionResult Delete(int movieId)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var movie = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MovieId == movieId).FirstOrDefault();

            if (movie != null)
            {
                cartRepositery.Delete(movie);
                cartRepositery.Commit();

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            return RedirectToAction("NotFoundPage", "Home");
        }
        public IActionResult Pay()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var cartProduct = cartRepositery.GetAll([e => e.Movie], e => e.ApplicationUserId == ApplicationUserId).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in cartProduct)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Movie.Name,
                        },
                        UnitAmount = (long)item.Movie.Price * 100,
                    },
                    Quantity = item.count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

    }
}
