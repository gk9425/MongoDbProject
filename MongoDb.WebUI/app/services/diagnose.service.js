/*
 * The purpose of this service to diagnose the connection to the mongo t
 */
(function () {
    'use strict';

    angular
        .module('app')
        .factory('DiagnoseService', DiagnoseService);

    DiagnoseService.$inject = ['$http', '$q', 'appConfig'];

    function DiagnoseService($http, $q, appConfig) {
        var baserUrl = appConfig.baseUrl + "api/Diagnose/"; //URL of the service API
        var service = {
            testDatabaseConnection: testDatabaseConnection
        };

        return service;

        /*
         * This function is used for testing the connection to the database server
         */
        function testDatabaseConnection() {
            var url = baserUrl + 'GetDatabaseConnectionStatus';
            var deferred = $q.defer();
            $http.get(url)
                .success (function(data) {
                    deferred.resolve (data);
                })
                .error (function(data, status) {
                    deferred.reject (status);
                });
            return deferred.promise;
        }
    }
})();