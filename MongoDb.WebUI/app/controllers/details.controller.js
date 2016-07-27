/*
 * This angular controller handle the operation related to the detail views. It handles the add review operation
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('DetailsController', DetailsController);

    DetailsController.$inject = ['$stateParams', 'RestaurantService', '$state', '$filter'];

    function DetailsController($stateParams, RestaurantService, $state, $filter) {
        /* jshint validthis:true */
        var vm = this;
        //Rating
        vm.max = 5;
        vm.isReadonly = true;

        vm.restaurant = {};
        vm.comment = {
            text: ""
        };        

        //for pagination
        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.getNumberOfPages = getNumberOfPages;
        vm.offset = vm.currentPage * vm.pageSize;

        //Add new comment
        vm.isAddComment = false;
        vm.cancelComment = cancelComment;
        vm.addNewComment = addNewComment;
        vm.saveComment = saveComment;

        /*
         * This function enable pagination for the comments
         */
        vm.paginate = function (value) {
            var begin, end, index;
            begin = (vm.currentPage - 1) * vm.pageSize;
            end = begin + vm.pageSize;
            index = vm.restaurant.reviews.indexOf(value);
            return (begin <= index && index < end);
        };

        activate();

        /*
         * This functions is executed when this controller gets intatantiated 
         */
        function activate() {
            //console.log($stateParams);
            RestaurantService.getRestaurantById($stateParams.restaurandId).then(function (data) {
                vm.restaurant = data;
            });
        }

        function getNumberOfPages() {
            return Math.ceil(vm.restaurant.reviews.length / vm.pageSize);
        }

        /*
         * Cancel the current comment.
         */
        function cancelComment() {
            vm.isAddComment = false;
            vm.comment = {};
        }

        /*
         * This function enable the user control for adding a new comment to the database
         */
        function addNewComment() {
            vm.isAddComment = true;
        }

        /*
         * Save the current comment to the database
         */
        function saveComment(restaurant) {
            //add neede properties to the comment object
            vm.comment.restaurantId = restaurant.id;
            vm.comment.businessId = restaurant.businessId;
            RestaurantService.addReviewToRestaurant(vm.comment).then(function () {
                vm.isAddComment = false;
                //reload the view so we can view the new added comment
                $state.reload();
            });
        }

    }
})();
