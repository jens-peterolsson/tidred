adminApp.controller('customerEditController', ["$scope", "adminService", "urls", "$timeout",
    function ($scope, adminService, urls, $timeout) {

        $scope.urls = urls;
        $scope.currencies = adminService.currencies;
        $scope.customer = adminService.selectedCustomer;
        $scope.statusMessageShow = false;

        $scope.save = function(form) {
            if (form.$valid) {
                adminService.saveCustomer();
            }
        };

        $scope.$on('customerUpdate', function () {
            $scope.statusMessage = "Customer updated.";
            $scope.statusMessageShow = true;
            $timeout(function() {
                $scope.statusMessageShow = false;
            }, 1200);
        });

    }]);