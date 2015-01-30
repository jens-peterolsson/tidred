timeApp.controller('timeRecordEditController', ["$scope", "adminService", "urls", "$location",
    function ($scope, adminService, urls, $location) {

        $scope.urls = urls;
        $scope.currencies = adminService.currencies;
        $scope.customer = adminService.selectedCustomer;
        $scope.statusMessageShow = false;

        $scope.save = function(form) {
            if (form.$valid) {
                adminService.saveCustomer();
            }
        };

        $scope.cancel = function () {
            adminService.selectedCustomer = {};
            $location.path(urls.customer);
        };

        $scope.$on('customerSaved', function () {
            $location.path(urls.customer);
        });

    }]);