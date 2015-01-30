timeApp.factory('timeService', ["$http", "$rootScope", function($http, $rootScope) {

    var service = {};

    service.coId = accountHandler.getUserInfo().coId;
    service.timeRecords = [];
    service.priceTypes = [];
    service.customers = [];
    service.projectss = [];
    service.selectedTimeRecord = {};
    service.updateError = "";
    service.statusMessage = "";

    function getCustomers() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/customers",
            headers: headerInfo,
            params: { coId: service.coId }
        };

        $http(request)
            .then(function(result) {
                service.customers = result.data;
                $rootScope.$broadcast("customerUpdate");
            }
        );
    }

    function getPriceTypes() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/timerecords/pricetypes",
            headers: headerInfo,
            params: { coId: service.coId }
        };

        $http(request)
            .then(function (result) {
                service.priceTypes = result.data;
                $rootScope.$broadcast("priceTypeUpdate");
            }
        );
    }

    function getProjects() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/projects",
            headers: headerInfo,
            params: { coId: service.coId }
        };

        $http(request)
            .then(function (result) {
                service.projects = result.data;
                $rootScope.$broadcast("projectUpdate");
            }
        );
    }

    function getTimeRecords() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/timerecords",
            headers: headerInfo,
            params: { coId: service.coId }
        };

        //$http(request)
        //    .then(function (result) {
        //        service.priceTypes = result.data;
        //        $rootScope.$broadcast("priceTypeUpdate");
        //    }
        //);
    }

    service.saveTimeRecord = function () {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/timerecords",
            headers: headerInfo,
            data: service.selectedTimeRecord
        };

        $http(request)
            .then(function () {
                service.selectedCustomer = {};
                service.statusMessage = "Record saved.";
                $rootScope.$broadcast("timeRecordSaved");
                $rootScope.$broadcast("statusUpdate");
                getTimeRecords();
            }
        );

    };



    getCustomers();
    getProjects();
    getPriceTypes();
    getTimeRecords();

    return service;

}]);
