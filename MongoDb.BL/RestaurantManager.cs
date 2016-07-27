using System;
using System.Linq;
using MongoDb.BL.Utils;
using MongoDb.DAL.DTO;
using MongoDb.DAL.Model;
using MongoDb.DAL.Repositories;
using MongoDB.Bson;

namespace MongoDb.BL
{
    /// <summary>
    /// This class handle the business logic process, it formats and prepare information to be handle by the diffrents layer of the
    /// application.
    /// </summary>
    public class RestaurantManager
    {
        private readonly RestaurantRepository _restaurantRepository;

        /// <summary>
        /// Class constructor
        /// </summary>
        public RestaurantManager()
        {
            _restaurantRepository = new RestaurantRepository();
        }

        /// <summary>
        /// This method handles the search function. It provides pagination funtionality
        /// </summary>
        /// <param name="searchString">Search criteria</param>
        /// <param name="pageIndex">For paging</param>
        /// <param name="pageSize">For pagin</param>
        /// <returns>RestaurantPartialView</returns>
        public RestaurantPartialView GetRestaurants(string searchString, int pageIndex = 0, int pageSize = 10)
        {
            var restaurants = _restaurantRepository.GetRestaurants(searchString, pageIndex, pageSize);
            int restaurantCount = _restaurantRepository.CountRestaurantBySearchString(searchString);
            return new RestaurantPartialView
            {
                Restaurants = restaurants,
                RestaurantCount = restaurantCount
            };
        }

        /// <summary>
        /// Gets a restaurant given the Id, it also adds a random image to the restaurant
        /// </summary>
        /// <param name="restaurantId">Id of the restaurant</param>
        /// <returns>a single Restaurant object with a random picture</returns>
        public Restaurant GetRestaurantId(string restaurantId)
        {
            var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
            //add random picture
            restaurant.ImageUrl = RestaurantImage.GenerateImageUrl();
            return restaurant;
        }

        /// <summary>
        /// Update a restaurant by adding review. It performs the creation of the review object and pass it to the data layer.
        /// </summary>
        /// <param name="restaurantId">Id of the restaurant to be updated</param>
        /// <param name="review">Partial object coming from the graphical user interface</param>
        /// <returns>Boolean</returns>
        public bool UpdateReviewByRestaurantId(string restaurantId, ReviewPartial review)
        {
            Review newReview = new Review
            {
                Id = ObjectId.GenerateNewId(),
                BusinessId = review.BusinessId,
                Text = review.Text,
                User = "Annonymous",
                CreatedOn = DateTime.Now,
                Stars = 5 //implement?
            };
            return _restaurantRepository.AddReviewtoRestaurant(restaurantId, newReview);
        }
    }
}