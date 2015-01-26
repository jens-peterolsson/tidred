adminApp.controller('projectController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {
        $scope.urls = urls;
    }]);