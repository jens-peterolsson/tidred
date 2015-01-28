adminApp.controller('userCreateController', ["$scope", "adminService", "$timeout",
    function ($scope, adminService, $timeout) {

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

        $scope.$on('userUpdate', function () {
            $scope.statusMessage = "User updated.";
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 1200);
        });

        $scope.$on('updateError', function () {
            $scope.statusMessage = adminService.updateError;
            $scope.statusMessageShow = true;
            $timeout(function () {
                $scope.statusMessageShow = false;
            }, 3600);
        });

    }]);