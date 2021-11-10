using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Models.Rating
{
    public class RatingDelete
    {
        public int RatingId { get; set; }
        [Display(Name = "Restaurant")]
        public string RestaurantName { get; set; }
        public double Score { get; set; }
    }
}