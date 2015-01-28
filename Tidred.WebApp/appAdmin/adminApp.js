var adminApp = angular.module('adminApp', ["ngRoute"])
    .constant("urls", {
        "user": "/adminApp/user",
        "userCreate": "/adminApp/user/create",
        "project": "/adminApp/project",
        "customer": "/adminApp/customer",
        "customerEdit": "/adminApp/customer/edit",
        "customerCreate": "/adminApp/customer/create",
        "projectEdit": "/adminApp/project/edit",
        "projectCreate": "/adminApp/project/create"
    }
);

adminApp.config(["$routeProvider", "urls", function($routeProvider, urls) {

    $routeProvider
        .when(urls.user, {
            templateUrl: "/Admin/UserPartial",
            controller: "userController"
        })
        .when(urls.userCreate, {
            templateUrl: "/Admin/UserCreatePartial",
            controller: "userCreateController"
        })
        .when(urls.project, {
            templateUrl: "/Admin/ProjectPartial",
            controller: "projectController"
        })
        .when(urls.projectEdit, {
            templateUrl: "/Admin/ProjectEditPartial",
            controller: "projectEditController"
        })
        .when(urls.projectCreate, {
            templateUrl: "/Admin/ProjectEditPartial",
            controller: "projectEditController"
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