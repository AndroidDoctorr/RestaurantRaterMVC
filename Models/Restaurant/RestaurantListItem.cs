using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Models.Restaurant
{
    public class RestaurantListItem
    {
        public string Name { get; set; }
        [Display(Name = "Rating")]
        public double Score { get; set; }
    }
}