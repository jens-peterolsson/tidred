adminApp.controller('adminController', ["$scope", "adminService",
    function ($scope, adminService) {
        $scope.urlUser = "/adminApp/user";
        $scope.urlProject = "/adminApp/project";
        $scope.urlCustomer = "/adminApp/customer";
    }]);