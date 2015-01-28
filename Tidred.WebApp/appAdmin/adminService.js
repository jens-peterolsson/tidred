adminApp.factory('adminService', ["$http", "$rootScope", function($http, $rootScope) {

    var service = {};

    service.coId = accountHandler.getUserInfo().coId;
    service.customers = [];
    service.projects = [];
    service.priceTypes = [];
    service.users = [];
    service.currencies = [];
    service.selectedCustomer = {};
    service.selectedProject = {};
    service.newUser = {};
    service.updateError = "";

    function getCurrencies() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/customers/currencies",
            headers: headerInfo
        };

        $http(request)
            .then(function (result) {
                service.currencies = result.data;
                $rootScope.$broadcast("currencyUpdate");
            }
        );

    }

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

    service.saveCustomer = function() {

        service.selectedCustomer.coId = service.coId;

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/customers",
            headers: headerInfo,
            data: service.selectedCustomer
        };

        $http(request)
            .then(function () {
                service.selectedCustomer = {};
                getCustomers();
            }
        );

    };

    function getPriceTypes() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/projects/pricetypes",
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

    function getUsers() {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "GET",
            url: "api/users",
            headers: headerInfo,
            params: { coId: service.coId }
        };

        $http(request)
            .then(function (result) {
                service.users = result.data;
                $rootScope.$broadcast("userUpdate");
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

    service.saveProject = function () {

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/projects",
            headers: headerInfo,
            data: service.selectedProject
        };

        $http(request)
            .then(function () {
                service.selectedProject = {};
                getProjects();
            }
        );

    };

    service.saveUser = function () {

        service.newUser.coId = service.coId;

        var headerInfo = accountHandler.getAccountHeader();

        var request = {
            method: "POST",
            url: "api/account/register",
            headers: headerInfo,
            data: service.newUser
        };

        $http(request)
            .then(function () {
                service.newUser = {};
                getUsers();
            }, function (err) {
                service.updateError = err.data.message;
                $rootScope.$broadcast("updateError");
            }
        );

    };


    getCustomers();
    getCurrencies();
    getProjects();
    getPriceTypes();
    getUsers();

    return service;

}]);
