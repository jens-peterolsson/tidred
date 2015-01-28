adminApp.controller('userController', ["$scope", "adminService", "urls", 
    function ($scope, adminService, urls) {

        $scope.urls = urls;

        $scope.users = adminService.users;

        $scope.$on('userUpdate', function () {
            $scope.users = adminService.users;
        });

    }]);