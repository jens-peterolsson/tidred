var adminApp = angular.module('adminApp', ["ngRoute", "ui.bootstrap"]);

adminApp.config(["$routeProvider", function($routeProvider) {

    // "/adminApp/projects/:id" för att skicka argument id i routingen... kräver $routeParams för controller
    // behövs locationprovider och html5mode?
    $routeProvider
        .when("/adminApp/x", {
            templateUrl: "/Admin/x",
            controller: "x"
        })
        .when("/adminApp/y", {
            templateUrl: "/Admin/y",
            controller: "y"
        })
        .otherwise({
            redirectTo: "/"
    });

}]);