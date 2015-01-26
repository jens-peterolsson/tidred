var adminApp = angular.module('adminApp', ["ngRoute"])
    .constant("urls", {
        "user": "/adminApp/user",
        "project": "/adminApp/project",
        "customer": "/adminApp/customer"
    }
);

adminApp.config(["$routeProvider", "urls", function($routeProvider, urls) {

    // "/adminApp/projects/:id" för att skicka argument id i routingen... kräver $routeParams för controller
    $routeProvider
        .when(urls.user, {
            templateUrl: "/Admin/UserPartial",
            controller: "userController"
        })
        .when(urls.project, {
            templateUrl: "/Admin/ProjectPartial",
            controller: "projectController"
        })
        .when(urls.customer, {
            templateUrl: "/Admin/CustomerPartial",
            controller: "customerController"
        })
    //    .otherwise({
    //        redirectTo: "/"
    //})
    ;

}]);