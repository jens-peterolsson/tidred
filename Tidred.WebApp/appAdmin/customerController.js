adminApp.controller('customerController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {
        $scope.urls = urls;
    }]);