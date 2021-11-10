using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _context;
        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Restaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                // Ratings = _context.Ratings.Where(r => r.RestaurantId == id).Select(r => r.Score).ToList(),
                Ratings = restaurant.Ratings.Select(r => r.Score).ToList(),
            };
            return View(restaurantDetail);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errorMessage = ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage;
                return View(model);
            }

            Restaurant restaurant = new Restaurant()
            {
                Name = model.Name,
                Location = model.Location,
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
            };
            return View(restaurant);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return View(model);

            restaurant.Name = model.Name;
            restaurant.Location = model.Location;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
            };
            return View(restaurantEdit);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}