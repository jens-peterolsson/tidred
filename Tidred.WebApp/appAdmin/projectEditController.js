adminApp.controller('projectEditController', ["$scope", "adminService", "urls", "$timeout", "$location",
    function ($scope, adminService, urls, $timeout, $location) {

        $scope.urls = urls;
        $scope.customers = adminService.customers;
        $scope.project = adminService.selectedProject;
        $scope.statusMessageShow = false;

        $scope.save = function (form) {
            if (form.$valid) {
                adminService.saveProject();
            }
        };

        $scope.cancel = function() {
            adminService.selectedProject = {};
            $location.path(urls.project);
        };

        $scope.$on('projectSaved', function () {
            $location.path(urls.project);
        });

    }]);