adminApp.controller('customerController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {

        $scope.urls = urls;

        $scope.customers = adminService.customers;

        $scope.$on('customerUpdate', function () {
            $scope.customers = adminService.customers;
        });

        $scope.setCustomer = function(customer) {
            adminService.selectedCustomer = angular.copy(customer);
        };

    }]);