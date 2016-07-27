using MongoDb.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.DAL.DTO
{
    public class RandomImageMapper
    {
        private int requiredImageCount;

        //Imp:  path should end with forward slash ~images/ 
        
        public string ImageFolderPath { get; set; }

        private List<Restaurant> targetRestaurantList;
        
        public int TotalImageCount { get; set; }
               
        
        //constructor accepting the list
        public void RandomImageMapper(List<Restaurant> sourceRestaurantList)
        {
            this.targetRestaurantList = sourceRestaurantList;
            requiredImageCount = sourceRestaurantList.Count<Restaurant>();
        }


        //constructor accepting a single object
        public void RandomImageMapper(Restaurant sourceRestaurant)
        {
            this.targetRestaurantList = new List<Restaurant>();
            ((List<Restaurant>)this.targetRestaurantList).Add(sourceRestaurant);
            requiredImageCount = 1;
        }


        // Method used to assign image url
        public IEnumerable<Restaurant> MapImages()
        {
            List<int> randomNumList = GenerateRandomNumbers();
            int counter = 0;

            // Realtive path need to be assigned accordingly
           
            foreach (Restaurant objRest in this.targetRestaurantList)
            {
                objRest.ImageUrl = ImageFolderPath + "r"+ randomNumList[counter].ToString() + ".jpg";
                counter = counter + 1;
            }

            return this.targetRestaurantList;
        }



        // Method used to assign image url to a single object
        public Restaurant MapSingleImage()
        {
            List<int> randomNumList = GenerateRandomNumbers();
            int counter = 0;

            // Realtive path need to be assigned accordingly

            foreach (Restaurant objRest in this.targetRestaurantList)
            {
                objRest.ImageUrl = ImageFolderPath + randomNumList[counter].ToString() + ".png";
                counter = counter + 1;
            }

            return this.targetRestaurantList.First();
        }


        //Random list generation
        private List<int> GenerateRandomNumbers()
        {
            Int32[] numberArrays = new Int32[TotalImageCount];

            for (int i = 1; i < TotalImageCount; i++)
            {
                numberArrays[i] = i;
            }

            Random rng = new Random();
            int n = TotalImageCount;
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                int temp = numberArrays[n];
                numberArrays[n] = numberArrays[k];
                numberArrays[k] = temp;
            }

            return numberArrays.Take(requiredImageCount).ToList();

        }
    }
}
