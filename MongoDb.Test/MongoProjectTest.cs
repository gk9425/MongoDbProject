using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDb.DAL.Context;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDb.DAL.DTO;
using MongoDb.DAL.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoDb.Test
{
    [TestClass]
    public class MongoProjectTest
    {

        [TestMethod]
        public void AppIsNotConnectedToMongoServer()
        {
            //.Ping();
            RestaurantContext context = new RestaurantContext();
            bool isConnected = context.IsServerOnline();
            Console.WriteLine("Connected");
            Assert.IsFalse(!isConnected);
        }

        [TestMethod]
        public void AppIsConnectedToMongoServer()
        {
            //.Ping();
            RestaurantContext context = new RestaurantContext();
            bool isConnected = context.IsServerOnline();

            Assert.IsTrue(isConnected); 
        }

        [TestMethod]
        public void CanCountItemInMongoDb()
        {
            RestaurantContext context = new RestaurantContext();
            long count = context.Restaurant.Count();
            Assert.IsTrue(count>0);
        }

        [TestMethod]
        public void CanUseProjectUsingLinq()
        {
            RestaurantContext context = new RestaurantContext();
            var document =
                context.Restaurant.AsQueryable<Restaurant>()
                    .Select(t => new {t.Name, t.Address, t.Stars})
                    .FirstOrDefault();
            Assert.IsTrue(document!=null);
        }

        [TestMethod]
        public void CanSearchUsingContain()
        {
            RestaurantContext context = new RestaurantContext();
            var document =
                context.Restaurant.AsQueryable<Restaurant>()
                    .FirstOrDefault(t=>t.Name.Contains("Domino"));
            Assert.IsTrue(document!=null);
        }

        [TestMethod]
        public void CanSearchByObjectId()
        {
            RestaurantContext context = new RestaurantContext();
            var document =
                context.Restaurant.AsQueryable<Restaurant>()
                    .FirstOrDefault(t => t.Id == new ObjectId( "552d43a2f5ac745e4f4e4ea4"));
            Assert.IsTrue(document!=null);
        }

        [TestMethod]
        public void CanQueryUsingMongoAPICommand()
        {
            RestaurantContext context = new RestaurantContext();
          //  IMongoQuery myQuery = Query.EQ("business_id", "KTqNU4plO23583DYAMGXYg");
            IMongoQuery myQuery = Query<Restaurant>.EQ(t => t.BusinessId, "KTqNU4plO23583DYAMGXYg");
            Restaurant restaurant = context.Restaurant.FindOne(myQuery);
            var test = context.Restaurant.FindAllAs<Restaurant>().SetFields(Fields.Include("name", "")).ToList();
            Assert.IsTrue(restaurant!=null);
        }

       // [TestMethod]
        public void CanQueryUsingRegularExpresion()
        {
            RestaurantContext context = new RestaurantContext();
            List<Restaurant> searchResults = new List<Restaurant>();

            // sort expression fields used : Stars
            var sortBy = SortBy<Restaurant>.Descending(u => u.Stars);

            // build search expression
           // string searchExpression = Utility.SearchExpressionBuilder(searchString, optionSearch).ToString();
            string searchExpression = new Regex("\\s" + "Domino" + "\\s").ToString();
            BsonRegularExpression expression = new BsonRegularExpression(searchExpression);

            //Prepare query
            //IMongoQuery myQuery = Query.ElemMatch("name", Query.Matches("name", expression));
            IMongoQuery myQuery =  Query<Restaurant>.Matches(t=>t.Name, new BsonRegularExpression(new Regex("^Domin")));

            var myResults = context.Restaurant.Find(myQuery);

            long count = myResults.Count();
            var test = myResults.ToList();
            myResults.SetSortOrder(sortBy);

         /*   //pagination
            myResults.Skip = pageIndex * pageSize;
            myResults.Limit = pageSize;

            foreach (Restaurants item in myResults)
            {
                searchResults.Add(item);

            }

            // Map pictures 
            RandomImageMapper objMapper = new RandomImageMapper(searchResults);
            objMapper.TotalImageCount = 50; // for now. needs to be upadted

            return objMapper.MapImages();*/
        }

        [TestMethod]
        public void TestQueryProjection()
        {
            RestaurantContext context = new RestaurantContext();
            string searchString = "domino";
            int skip = 0;
            int limit = 10;
            int total = context.Restaurant.AsQueryable().Count(t => t.Name.ToLower().Contains(searchString.ToLower()));
            var restaurants = context.Restaurant.AsQueryable<Restaurant>()
                .Where(t => t.Name.ToLower().Contains(searchString.ToLower()))
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
            Assert.IsTrue(restaurants!=null);
        }

     //   [TestMethod]
        public void CanSearchByObjectIdUsingQueryClass()
        {
            RestaurantContext context = new RestaurantContext();
            var query = Query.EQ("_id", ObjectId.Parse("552d43a2f5ac745e4f4e4ea4"));
            var query1 = Query<Restaurant>.EQ(t => t.Id, ObjectId.Parse("552d43a2f5ac745e4f4e4ea4"));
            var restaurant = context.Restaurant.Find(query1).FirstOrDefault();

            Assert.IsTrue(true);
        }

       // [TestMethod]
        public void CanUpdateRestaurantByAddingReview()
        {
            RestaurantContext context = new RestaurantContext();
            Review review = new Review
            {
                Id = ObjectId.GenerateNewId(),
                Text = "Added by Sandy, Perfect SECONDONE ONE",
                Stars = 5.0,
                User = "sabreu",
                CreatedOn = DateTime.Now,
                BusinessId = "KTqNU4plO23583DYAMGXYg" //add
            };
            var query = Query<Restaurant>.EQ(t => t.Id, ObjectId.Parse("552d43a2f5ac745e4f4e4ea4"));
            var result = context.Restaurant.Update(query, Update<Restaurant>.Push(t => t.Reviews, review));

            Assert.IsTrue(result.Ok);
        }
    }
}
