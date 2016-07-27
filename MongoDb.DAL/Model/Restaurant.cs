using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.DAL.Model
{
    public class Restaurant
    {
        private ICollection<Review> _reviews; 

        public ObjectId Id { get; set; }

        [BsonElement("business_id")]
        public string BusinessId { get; set; }

        [BsonElement("full_address")]
        public string Address { get; set; }

        [BsonElement("categories")]
        public ICollection<string> Categories { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("longitude")]
        [BsonRepresentation(BsonType.Double)]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        [BsonRepresentation(BsonType.Double)]
        public double Latitude { get; set; }

        [BsonElement("attributes")]
        public RestaurantAttribute RestaurantAttribute { get; set; }

        [BsonElement("reviews")]
        public ICollection<Review> Reviews
        {
            set { _reviews = value; }
            get { return _reviews.OrderByDescending(t => t.CreatedOn).ToList(); }
        }

        [BsonElement("stars")]
        [BsonRepresentation(BsonType.Double)]
        public double Stars { get; set; }

        // Property to be populated later : Gaurav
        public string ImageUrl { get; set; }


    }
}