adminApp.controller('userController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {
        $scope.urls = urls;
    }]);