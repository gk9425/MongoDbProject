using System;
using MongoDb.DAL.Model;
using MongoDB.Driver;

namespace MongoDb.DAL.Context
{
    /// <summary>
    /// The purpose of this class is to encapsulate the connection to the mongo database. According to the mongo documentation we don't need
    /// to close connection because that behavior is handle by the mongo driver itself.
    /// </summary>
    public class RestaurantContext
    {
        const string CONNECTIONSTRING = "mongodb://localhost";
        const string DATABASENAME = "Restaurant";
        //Property to hold the current database instantance 
        private readonly MongoDatabase _restaurantDb;
        private readonly MongoServer _server;
        public RestaurantContext()
        {
            try
            {
                MongoClient client = new MongoClient(CONNECTIONSTRING);
                _server = client.GetServer();
                _restaurantDb = _server.GetDatabase(DATABASENAME);
            }
            catch (MongoConnectionException mce)
            {
                throw mce;
            }
        }

        /// <summary>
        /// This property represents the Restaurants collection
        /// </summary>
        public MongoCollection<Restaurant> Restaurant
        {
            get
            {
                return _restaurantDb.GetCollection<Restaurant>("Restaurants");
            }
        }

        /// <summary>
        /// Method for testing connection to the mongodb server.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsServerOnline()
        {
            try
            {
                _server.Ping();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}