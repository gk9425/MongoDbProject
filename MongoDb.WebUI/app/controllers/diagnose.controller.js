(function () {
    'use strict';

    angular
        .module('app')
        .controller('DiagnoseController', DiagnoseController);

    DiagnoseController.$inject = ['$state', '$timeout', 'DiagnoseService', '$rootScope'];

    function DiagnoseController($state, $timeout, DiagnoseService, $rootScope) {
        /* jshint validthis:true */
        var vm = this;
        vm.statusMessage = 'Connecting to MongoDB server, Restaurant database and Restaurants collection';
        vm.isServerOffline = false;

        activate();

        function activate() {
            DiagnoseService.testDatabaseConnection().then (function(data) {
                if (data) {
                    vm.statusMessage = 'Connected to the Restaurant database and Restaurants collection, redirecting to search page';
                    vm.isServerOffline = false;
                    $timeout(function () {
                        $state.go('index');
                    }, 3000);
                    
                } else {
                    onConnectionFailed();
                    vm.isServerOffline = true;
                    vm.statusMessage = 'Mongo database server is offline';
                }
            });         
        }

        function onConnectionFailed() {
             $rootScope.$on('$stateChangeStart',
            function (event, toState, toParams, fromState, fromParams) {
                event.preventDefault();
                // transitionTo() promise will be rejected with 
                // a 'transition prevented' error
            });
        }

    }
})();
