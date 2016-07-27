using System.Collections.Generic;

namespace MongoDb.DAL.DTO
{
    public class RestaurantPartialView
    {
        public int RestaurantCount { get; set; }
        public IEnumerable<RestaurantPartial> Restaurants { get; set; }
    }
}