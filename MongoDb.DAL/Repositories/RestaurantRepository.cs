using System;
using System.Collections.Generic;
using System.Linq;
using MongoDb.DAL.Context;
using MongoDb.DAL.DTO;
using MongoDb.DAL.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoDb.DAL.Repositories
{
    public class RestaurantRepository
    {
        private readonly RestaurantContext _context;
        /// <summary>
        /// Class Constructor
        /// </summary>
        public RestaurantRepository()
        {
            _context = new RestaurantContext();
        }

        /// <summary>
        /// Return the list of restaurants to be displayed as the search result. The partial class only shows the field needed 
        /// </summary>
        /// <param name="searchString">Is param used to search</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>IEnumerable of Restaurants Partial</returns>
        public IEnumerable<RestaurantPartial> GetRestaurants(string searchString, int pageIndex, int pageSize) 
        {
            try
            {
                int skip = pageIndex * pageSize; //number of records to be skipped
                int limit = pageSize; //Amount of record
                var restaurants = _context.Restaurant.AsQueryable<Restaurant>()
                   .Where(t => t.Reviews.Any(d => d.Text.ToLower().Contains(searchString.ToLower())))
                    .Skip(skip)
                    .Take(limit)
                    .Select(t => new RestaurantPartial
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Address = t.Address,
                        Categories = t.Categories,
                        Rating = t.Stars
                    })
                    .ToList();
                return restaurants;
            }
            catch (MongoConnectionException mce)
            {
                
                throw mce;
            }
        }

        /// <summary>
        /// Counts the number of restaurant that match a given searchString
        /// </summary>
        /// <param name="searchString">Param used to search</param>
        /// <returns>integer</returns>
        public int CountRestaurantBySearchString(string searchString)
        {
            //return _context.Restaurant.AsQueryable().Count(t => t.Name.ToLower().Contains(searchString.ToLower()));
            return
                _context.Restaurant.AsQueryable()
                    .Count(t => t.Reviews.Any(d => d.Text.ToLower().Contains(searchString.ToLower())));
        }

        /// <summary>
        /// Gets a restaurant from the database given the ObjectId of the document
        /// </summary>
        /// <param name="id">ObjectId of the document</param>
        /// <returns>Restaurants object</returns>
        public Restaurant GetRestaurantById(string id)
        {
            var restaurantId = ObjectId.Parse(id);
            return _context.Restaurant.AsQueryable().FirstOrDefault(t => t.Id == restaurantId);
        }

        /// <summary>
        /// Updates a restaurant by adding one new review to reviews array.
        /// </summary>
        /// <param name="id">ObjectId of the restaurant</param>
        /// <param name="newReview">New review object to be added (Pusht to the reviews array)</param>
        /// <returns>Boolean</returns>
        public bool AddReviewtoRestaurant(string id, Review newReview)
        {
            var restaurantId = ObjectId.Parse(id);
            var query = Query<Restaurant>.EQ(t=>t.Id, restaurantId);
            var result = _context.Restaurant.Update(query, Update<Restaurant>.Push(t => t.Reviews, newReview));
            if (!result.Ok)
                return false;
            return true;
        }
    }
}