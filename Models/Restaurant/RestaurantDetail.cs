using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Models.Restaurant
{
    public class RestaurantDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Score { get; set; }
    }
}