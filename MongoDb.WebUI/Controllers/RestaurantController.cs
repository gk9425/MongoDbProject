using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDb.BL;
using MongoDb.DAL.DTO;

namespace MongoDb.WebUI.Controllers
{
    /// <summary>
    /// The purpose of this API is to expose data access functionality to the angular client side application.
    /// </summary>
    public class RestaurantController : ApiController
    {
        private readonly RestaurantManager _restaurantManager;

        /// <summary>
        /// Class constructor
        /// </summary>
        public RestaurantController()
        {
            _restaurantManager = new RestaurantManager();
        }

        
        /// <summary>
        /// Gets all the restaurant that match with the given search criteria
        /// This action
        /// </summary>
        /// <param name="searchString">search string</param>
        /// <param name="page">For pagination</param>
        /// <returns>IEnumerable of partial restaurant</returns>
        [HttpGet]
        public IHttpActionResult GetRestaurants(string searchString, int page)
        {
            try
            {
                var result = _restaurantManager.GetRestaurants(searchString, page - 1);
                if (result != null && result.RestaurantCount > 0)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        /// <summary>
        /// Get a single restaurant from the database
        /// </summary>
        /// <param name="restaurantId">Id of the restaurant</param>
        /// <returns>Restaurant object</returns>
        [HttpGet]
        public IHttpActionResult GetRestaurant(string restaurantId)
        {
            var result = _restaurantManager.GetRestaurantId(restaurantId);
            if (result!=null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Updates a Restaurant by addion review to the reviews array
        /// </summary>
        /// <param name="review">Partial review information</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddReviewToRestaurant(ReviewPartial review)
        {
            var result = _restaurantManager.UpdateReviewByRestaurantId(review.RestaurantId, review);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to perform the operation");
        }

    }
}
