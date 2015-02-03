timeApp.factory('timeService', ["$http", "$rootScope", "$filter", function($http, $rootScope, $filter) {

    var service = {};

    var userInfo = accountHandler.getUserInfo();
    service.coId = userInfo.coId;
    service.userId = userInfo.userId;
    service.timeRecords = [];
    service.priceTypes = [];
    service.customers = [];
    service.projects = [];
    service.filteredProjects = [];
    service.selectedTimeRecord = {};
    service.updateError = "";
    service.statusMessage = "";
    service.customerId = "";
    service.projectId = "";
    service.userPrefs = {};

    service.format = "yyyy-MM-dd";

    var date = new Date();
    date.setDate(date.getDate() - 14);

    service.startDate = $filter('date')(date, service.format);
    service.endDate = $filter('date')(new Date(), service.format);

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
                service.filteredProjects = result.data;
                $rootScope.$broadcast("projectUpdate");
            }
        );
    }

    service.getTimeRecords = function (startDate, endDate, customerId, projectId) {

        service.timeRecords = {};
        service.startDate = startDate;
        service.endDate = endDate;
        service.customerId = customerId;
        service.projectId = projectId;

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

        service.selectedTimeRecord.userId = service.userId;
        service.selectedTimeRecord.timeEntryId = service.selectedTimeRecord.id;
        service.selectedTimeRecord.day = $filter('date')(service.selectedTimeRecord.day, service.format);

        var request = {
            method: "PUT",
            url: "api/timerecords",
            headers: headerInfo,
            data: service.selectedTimeRecord
        };

        $http(request)
            .then(function () {
                service.userPrefs.customerId = service.selectedTimeRecord.customerId;
                service.userPrefs.projectId = service.selectedTimeRecord.projectId;
                service.userPrefs.priceTypeId = service.selectedTimeRecord.priceTypeId;
                storeUserPrefs();
                service.selectedTimeRecord = {};
                service.statusMessage = "Record saved.";
                $rootScope.$broadcast("timeRecordSaved");
                $rootScope.$broadcast("statusUpdate");
            }
        );

    };

    service.filterProjectByCustomer = function(customerId, project) {
        if (project.customerId == customerId) {
            return true;
        } else {
            return false;
        }
    };

    function getUserPrefs() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/users/prefs",
            headers: headerInfo,
            params: { userId: service.userId }
        };

        $http(request)
            .then(function (result) {
                service.userPrefs = result.data;
            }
        );

    }

    function storeUserPrefs() {
        
        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "PUT",
            url: "api/users/prefs",
            headers: headerInfo,
            data: service.userPrefs
        };

        $http(request);
    }

    getCustomers();
    getProjects();
    getPriceTypes();
    getUserPrefs();

    return service;

}]);
