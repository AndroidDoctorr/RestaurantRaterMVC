using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestaurantRaterMVC.Data
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Display(Name = "Rating")]
        public double Score
        {
            get
            {
                return Ratings.Select(r => r.Score).Sum() / Ratings.Count;
            }
        }
        public virtual List<Rating> Ratings { get; set; }
    }
}