/*
 * this controller handles operations of the index view, it performs the search operation
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['RestaurantService', 'toastr', '$log'];

    function IndexController(RestaurantService, toastr, $log) {
        /* jshint validthis:true */
        var vm = this;
        
        vm.searchText = "";
        vm.currentPage = 0;
        vm.searchRestaurants = searchRestaurants;
        vm.keyPress = keyPress;
        //public exposed properties
        vm.restaurants = [];
        vm.isSearching = false;

        activate();

        //Pagination options
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.maxSize = 5; //Max number of page to display in the pagination control
        //Rating
        vm.max = 5;
        vm.isReadonly = true;

        /***
         * Function execute every time we interact with the pagination control
         */
        vm.pageChanged = function () {
            searchRestaurants();
        };

        function activate() {
        }

        /*
         * This function search for restaurants in the database.
         */
        function searchRestaurants() {
            vm.isSearching = true;
            RestaurantService.getRestaurants(vm.searchText, vm.currentPage).then(function (data) {
                if (data.restaurantCount>0) {
                  //  vm.searchText = "";
                    vm.totalItems = data.restaurantCount;
                    vm.restaurants = data.restaurants;
                    vm.isSearching = false;
                } else {
                    toastr.warning('Not record found');
                    vm.isSearching = false;
                }
            }, function (status) {
                console.log (status);
                if (status == 404) {
                    toastr.warning('Not record found');
                } else {                    
                    toastr.error('A server error has occurred. ERROR: ' + status);
                }
                vm.isSearching = false;

            });
        }

        /*
         * This function handle keypress event. If the enter key is pressed the searchRestaurants function is executed 
         */
        function keyPress($event) {
            if ($event.keyCode ===13) {
                searchRestaurants();
            }
          //  console.log($event.keyCode);
        }
    }
})();
