adminApp.controller('projectController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {

        $scope.urls = urls;

        $scope.projects = adminService.projects;

        $scope.$on('projectUpdate', function () {
            $scope.projects = adminService.projects;
        });

        $scope.setProject = function (project) {
            adminService.selectedProject = angular.copy(project);
        };

    }]);