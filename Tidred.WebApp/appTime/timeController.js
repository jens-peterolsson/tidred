timeApp.controller('timeController', ["$scope", "timeService", "urls", "$timeout", "$location",
    function ($scope, timeService, urls, $timeout, $location) {
        $scope.urls = urls;

        $scope.$on('statusUpdate', function () {
            $scope.statusMessage = timeService.statusMessage;
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 1500);
        });

        var path = $location.path();
        if (path == "/time" || path == "") {
            $location.path("/timeApp");
        }
    }

]);