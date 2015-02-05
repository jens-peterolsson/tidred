timeApp.controller('timeRecordController', ["$scope", "timeService", "urls", "$filter",
    function ($scope, timeService, urls, $filter) {

        $scope.urls = urls;

        $scope.startOpen = false;
        $scope.endOpen = false;
        $scope.format = timeService.format;
        
        $scope.startDate = timeService.startDate;
        $scope.endDate = timeService.endDate;
        $scope.customerId = timeService.customerId;
        $scope.projectId = timeService.projectId;
        $scope.customers = timeService.customers;
        $scope.projects = timeService.projects;

        $scope.timeRecords = timeService.timeRecords;

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
            $scope.startDate = $filter('date')($scope.startDate, $scope.format);
            $scope.endDate = $filter('date')($scope.endDate, $scope.format);
            timeService.getTimeRecords($scope.startDate, $scope.endDate, $scope.customerId, $scope.projectId);
        };

        $scope.projectFilter = function(project) {
            return timeService.filterProjectByCustomer($scope.customerId, project);
        };

        $scope.setTimeRecord = function (timeRecord) {
            timeService.selectedTimeRecord = angular.copy(timeRecord);
        };

        $scope.clearCustomer = function () {
            $scope.customerId = "";
        };

        $scope.clearProject = function () {
            $scope.projectId = "";
        };

        $scope.getTimeRecords();

    }]);