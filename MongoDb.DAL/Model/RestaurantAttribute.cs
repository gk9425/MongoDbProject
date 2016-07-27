using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.DAL.Model
{
    public class RestaurantAttribute
    {

        [BsonElement("outdoor_seating")]
        public bool IsOutdoorService { get; set; }

        [BsonElement("takes_reservations")]
        public bool IsTakeReservation { get; set; }

        [BsonElement("delivery")]
        public bool IsDeliveryAvailable { get; set; }

        [BsonElement("good_for_kid")]
        public bool IsGoodForKid { get; set; }

        [BsonElement("good_for_groups")]
        public bool IsGoodForGroup { get; set; }


        [BsonElement("has_TV")]
        public bool IsHasTV { get; set; }

        [BsonElement("take_out")]
        public bool IsTakeOut { get; set; }

        [BsonElement("attire")]
        public string Attire { get; set; }

        [BsonElement("caters")]
        public bool IsCaters { get; set; }

        [BsonElement("accept_credit_cards")]
        public bool IsAcceptCreditCards { get; set; }

        //waiter_service"
        [BsonElement("waiter_service")]
        public bool IsWaiterService { get; set; }


    }
}