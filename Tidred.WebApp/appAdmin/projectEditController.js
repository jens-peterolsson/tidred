adminApp.controller('projectEditController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {
        $scope.urls = urls;
    }]);