timeApp.factory('timeService', ["$http", "$rootScope", function($http, $rootScope) {

    var service = {};

    var userInfo = accountHandler.getUserInfo();
    service.coId = userInfo.coId;
    service.userId = userInfo.userId;
    service.timeRecords = [];
    service.priceTypes = [];
    service.customers = [];
    service.projects = [];
    service.selectedTimeRecord = {};
    service.updateError = "";
    service.statusMessage = "";
    service.initialized = false;

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

    service.getTimeRecords = function (startDate, endDate, customerId, projectId, onlyIfNotInitialized) {

        if (service.initialized && onlyIfNotInitialized) {
            return;
        }

        if (!service.initialized) {
            service.initialized = true;
        }

        service.timeRecords = {};

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/timerecords",
            headers: headerInfo,
            data: { userId: service.userId, start: startDate, end: endDate, customerId: customerId, projectId: projectId }
        };

        $http(request)
            .then(function (result) {
                service.timeRecords = result.data;
                $rootScope.$broadcast("timeRecordUpdate");
            }
        );
    }

    service.saveTimeRecord = function () {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "PUT",
            url: "api/timerecords",
            headers: headerInfo,
            data: service.selectedTimeRecord
        };

        $http(request)
            .then(function () {
                service.selectedTimeRecord = {};
                service.statusMessage = "Record saved.";
                $rootScope.$broadcast("timeRecordSaved");
                $rootScope.$broadcast("statusUpdate");
            }
        );

    };

    getCustomers();
    getProjects();
    getPriceTypes();

    return service;

}]);
