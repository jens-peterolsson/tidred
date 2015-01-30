timeApp.controller('timeRecordController', ["$scope", "adminService", "urls", "$timeout",
    function ($scope, adminService, urls, $timeout) {
        $scope.urls = urls;

        $scope.$on('statusUpdate', function () {
            $scope.statusMessage = adminService.statusMessage;
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 1500);
        });

    }]);