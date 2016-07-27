using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoDb.DAL.DTO
{
    public class RestaurantPartial
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public ICollection<String> Categories { get; set; }

        public string ImageUrl { get; set; }
    }
}