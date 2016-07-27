/*
 * This is the main module for the application. it defines the routing and load the module dependency
 */
(function () {
    'use strict';

    //obtain the base url of the application
    var baseUrl = window.location.protocol + '//' + window.location.host + '/'; //+ location.pathname; window.location.protocol + '//' + window.location.host + '/';
    var appConfig = {
        baseUrl: baseUrl
    };

    angular.module('app', [
            'ui.router', //Angular module for providing routing functionality.
            'ngAnimate', //Angular module for animation.
            'ngMessages', //Output Error Messages
            'toastr', //Angular module for providing a message functionality -  https://github.com/Foxandxss/angular-toastr
            'underscore', //Javascript library for managing collection of data
            'angular-loading-bar', //Display loading bar when XHR request are fired
            'ui.bootstrap.pagination', //Pagination
            'ui.bootstrap.rating' //Rating
    ])
        .config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider) {
            // For any unmatched url, redirect to /index
            $urlRouterProvider.otherwise("/");
            // Now set up the states
            $stateProvider
                .state('index', {
                    url: "/index",
                    controller: 'IndexController',
                    controllerAs: 'vm',
                    templateUrl: "app/views/index.html"
                })
                .state('details', {
                    url: "/details/:restaurandId",
                    controller: 'DetailsController',
                    controllerAs: 'vm',
                    templateUrl: "app/views/details.html"
                })
                .state('diagnose', {
                    url: '/diagnose',
                    controller: 'DiagnoseController',
                    controllerAs: 'vm',
                    templateUrl: "app/views/diagnose.html"
                });
        }])
        .config([
            '$compileProvider', function ($compileProvider) {
                $compileProvider.debugInfoEnabled(false); //false for production
            }
        ])
       .config(function (toastrConfig) {
           angular.extend(toastrConfig, {
               allowHtml: true
           });
       })
        .value('appConfig', appConfig)
    .run(function ($state) { //Run diagnose test on app startup
        $state.go('diagnose');
    })
    ; //Provide general settings to the application.;
})();