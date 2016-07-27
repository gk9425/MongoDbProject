using System;

namespace MongoDb.BL.Utils
{
    /// <summary>
    /// This class is used to generate random image URL to be displayed as part of the restaurant details.
    /// </summary>
    public class RestaurantImage
    {
        private const string IMAGE_URL = "../../images/";
        private const string IMAGE_PREFIX = "restaurant_";

        /// <summary>
        /// Generates a random image url to be displayed in the restaurant detail. It basically performs a concatenation
        /// of the image url, a fixed image name and a random number.
        /// </summary>
        /// <returns>String</returns>
        public static string GenerateImageUrl()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 11);
            return string.Format("{0}{1}{2}.jpg", IMAGE_URL, IMAGE_PREFIX, randomNumber);
        }
    }
}