using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.DAL.Model
{
    public class Review
    {
        public ObjectId Id { get; set; }

        [BsonElement("user_id")]
        public string User { get; set; }

        [BsonElement("date")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("stars")]
        [BsonRepresentation(BsonType.Double)]
        public double Stars { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("business_id")]
        public string BusinessId { get; set; }

    }
}