myApp.controller("usersCtrl", function ($scope, usersDataService, signalrFctr) {
    $scope.users = [];

    function update() {
        usersDataService.getUsers().success(function (data) {
            $scope.users = data;
        });
    }

    update();

});