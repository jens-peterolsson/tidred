timeApp.controller('timeRecordController', ["$scope", "timeService", "urls", "$filter",
    function ($scope, timeService, urls, $filter) {

        $scope.urls = urls;

        $scope.startOpen = false;
        $scope.endOpen = false;
        $scope.format = "yyyy-MM-dd";

        
        $scope.startDate = $filter('date')(new Date(2013, 2, 1), $scope.format);
        $scope.endDate = $filter('date')(new Date(), $scope.format);
        $scope.customerId = "";
        $scope.customers = timeService.customers;
        $scope.projects = timeService.projects;

        $scope.projectId = "";
        $scope.timeRecords = {};

        $scope.$on('timeRecordSaved', function () {
            $scope.getTimeRecords();
        });

        $scope.$on('timeRecordUpdate', function () {
            $scope.timeRecords = timeService.timeRecords;
        });

        $scope.$on('projectUpdate', function () {
            $scope.projects = timeService.projects;
        });

        $scope.$on('customerUpdate', function () {
            $scope.customers = timeService.customers;
        });

        $scope.getTimeRecords = function() {
            timeService.getTimeRecords($scope.startDate, $scope.endDate, $scope.customerId, $scope.projectId);
        };

        timeService.getTimeRecords($scope.startDate, $scope.endDate, $scope.customerId, $scope.projectId, true);

    }]);