adminApp.factory('adminService', ["$http", "$rootScope", function($http, $rootScope) {

    var service = {};

    service.coId = accountHandler.getUserInfo().coId;
    service.customers = [];
    service.currencies = [];
    service.selectedCustomer = {};

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
                getCustomers();
            }
        );

    };

    getCustomers();
    getCurrencies();

    return service;

}]);
