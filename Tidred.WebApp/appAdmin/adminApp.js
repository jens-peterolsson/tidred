var adminApp = angular.module('adminApp', ["ngRoute"])
    .constant("urls", {
        "user": "/adminApp/user",
        "project": "/adminApp/project",
        "customer": "/adminApp/customer",
        "customerEdit": "/adminApp/customer/edit",
        "customerCreate": "/adminApp/customer/create"
    }
);

adminApp.config(["$routeProvider", "urls", function($routeProvider, urls) {

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
        .when(urls.customerEdit, {
            templateUrl: "/Admin/CustomerEditPartial",
            controller: "customerEditController"
        })
        .when(urls.customerCreate, {
            templateUrl: "/Admin/CustomerEditPartial",
            controller: "customerEditController"
        })
    //    .otherwise({
    //        redirectTo: "/"
    //})
    ;

}]);