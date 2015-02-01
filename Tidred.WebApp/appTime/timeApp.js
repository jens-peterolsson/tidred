var timeApp = angular.module('timeApp', ["ngRoute", "ui.bootstrap", "ui.bootstrap.tpls"])
    .constant("urls", {
        "timeRecord": "/timeApp",
        "timeRecordEdit": "/timeApp/edit",
        "timeRecordCreate": "/timeApp/create"
    }
);

timeApp.config(["$routeProvider", "urls", function($routeProvider, urls) {

    $routeProvider
        .when(urls.timeRecord, {
            templateUrl: "/Time/TimePartial",
            controller: "timeRecordController"
        })
        .when(urls.timeRecordCreate, {
            templateUrl: "/Time/TimeEditPartial",
            controller: "timeRecordEditController"
        })
        .when(urls.timeRecordEdit, {
            templateUrl: "/Time/TimeEditPartial",
            controller: "timeRecordEditController"
        })
    //    .otherwise({
    //        redirectTo: "/"
    //})
    ;

}]);