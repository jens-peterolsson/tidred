timeApp.controller('timeRecordEditController', ["$scope", "timeService", "urls", "$location", "$filter",
    function ($scope, timeService, urls, $location, $filter) {

        $scope.urls = urls;
        $scope.projects = timeService.projects;
        $scope.customers = timeService.customers;
        $scope.priceTypes = timeService.priceTypes;

        $scope.timeRecord = timeService.selectedTimeRecord;
        $scope.format = "yyyy-MM-dd";
        $scope.opened = false;

        if (!$scope.timeRecord.id) {
            $scope.timeRecord.day = $filter('date')(new Date(), $scope.format);
            $scope.timeRecord.projectId = timeService.userPrefs.projectId;
            $scope.timeRecord.customerId = timeService.userPrefs.customerId;
            $scope.timeRecord.priceTypeId = timeService.userPrefs.priceTypeId;
        }

        $scope.statusMessageShow = false;

        $scope.save = function(form) {
            if (form.$valid) {
                timeService.saveTimeRecord();
            }
        };

        $scope.cancel = function () {
            timeService.selectedTimeRecord = {};
            $location.path(urls.timeRecord);
        };

        $scope.$on('timeRecordSaved', function () {
            $location.path(urls.timeRecord);
        });

        $scope.projectFilter = function (project) {
            return timeService.filterProjectByCustomer($scope.timeRecord.customerId, project);
        };

    }]);