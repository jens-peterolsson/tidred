adminApp.controller('projectEditController', ["$scope", "adminService", "urls", "$timeout",
    function ($scope, adminService, urls, $timeout) {

        $scope.urls = urls;
        $scope.customers = adminService.customers;
        $scope.project = adminService.selectedProject;
        $scope.statusMessageShow = false;

        $scope.save = function (form) {
            if (form.$valid) {
                adminService.saveProject();
            }
        };

        $scope.$on('projectUpdate', function () {
            $scope.statusMessage = "Project updated.";
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 1200);
        });


    }]);