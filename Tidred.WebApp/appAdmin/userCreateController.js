adminApp.controller('userCreateController', ["$scope", "adminService", "$timeout", "urls", "$location",
    function ($scope, adminService, $timeout, urls, $location) {

        $scope.$on('userUpdate', function () {
            $scope.users = adminService.users;
        });

        $scope.userName = "";
        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.statusMessageShow = false;

        $scope.setUser = function () {

            var user = {
                userName: $scope.userName,
                password: $scope.password,
                confirmPassword: $scope.confirmPassword
            };

            adminService.newUser = user;
            adminService.saveUser();
        };

        $scope.cancel = function () {
            adminService.selectedUser = {};
            $location.path(urls.user);
        };

        $scope.$on('userSaved', function () {
            $location.path(urls.user);
        });

        $scope.$on('updateError', function () {
            $scope.statusMessage = adminService.updateError;
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 3600);
        });

    }]);