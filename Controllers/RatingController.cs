using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Controllers
{
    public class RatingController : Controller
    {
        private RestaurantDbContext _context;
        public RatingController(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Rating> ratings = _context.Ratings;
            IEnumerable<RatingListItem> ratingList = ratings.Select(r => new RatingListItem()
            {
                RestaurantName = r.Restaurant.Name,
                Score = r.Score,
            });

            return View(ratingList);
        }

        public async Task<IActionResult> Restaurant(int id)
        {
            List<RatingListItem> ratings = _context.Ratings
            .Where(r => r.RestaurantId == id)
            .Select(r => new RatingListItem()
            {
                RestaurantName = r.Restaurant.Name,
                Score = r.Score,
            }).ToList();
            return View(ratings);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreate model)
        {
            Rating rating = new Rating()
            {
                RestaurantId = model.RestaurantId,
                Score = model.Score,
            };
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}