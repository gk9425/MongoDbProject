/*
 * This angular service handle the comunication to the service API. It uses the funtionality of the backend Web API
 */
(function () {
    'use strict';

    angular
        .module('app')
        .factory('RestaurantService', RestaurantService);

    RestaurantService.$inject = ['$http','$q', 'appConfig'];

    function RestaurantService($http, $q, appConfig) {
        var baserUrl = appConfig.baseUrl + "api/Restaurant/"; //URL of the service API
        //Public exposed functions
        var service = {
            getRestaurants: getRestaurants,
            getRestaurantById: getRestaurantById,
            addReviewToRestaurant: addReviewToRestaurant
        };

        return service;

        /*
         * This function get all the restaurants from the database that match a certain criteria
         * @param searchString String
         * @param page int
         * @return promise
         */
        function getRestaurants(searchString, page) {
            var url = baserUrl + 'GetRestaurants';
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: url,
                params: { searchString: searchString, page: page}
            }).success(function(data) {
                deferred.resolve (data);
            }).error(function (error, status) {
                deferred.reject (status);
            });
            return deferred.promise;
        }

        /*
         * Get a single restaurant from the database given its ID
         * @param restaurantId String
         * @return promise
         */
        function getRestaurantById(restaurantId) {
            var url = baserUrl + 'GetRestaurant';
            var deferred = $q.defer();
            $http ({
                method: 'GET',
                url: url,
                params: {restaurantId: restaurantId}
            }).success(function(data) {
                deferred.resolve (data);
            }).error(function(data, status) {
                deferred.reject (status);
            });
            return deferred.promise;
        }

        /*
         * Adds a review to a particular restaurant in the database
         * @param review Object
         * @return promise
         */
        function addReviewToRestaurant(review) {
            var url = baserUrl + 'AddReviewToRestaurant';
            var deferred = $q.defer();
            $http.post (url, review)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error (function(data, status) {
                    deferred.reject (status);
                });
            return deferred.promise;
        }
    }
})();