﻿adminApp.controller('adminController', ["$scope", "adminService", "urls",
    function ($scope, adminService, urls) {
        $scope.urls = urls;
    }]);