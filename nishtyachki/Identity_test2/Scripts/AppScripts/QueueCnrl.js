myApp.controller("QueueCntrl", function ($scope, QueueDataService) {
    $scope.queue = [];
    $scope.count = 1;
    $scope.roles = [{ value: 0, name: "standart" },
        { value: 1, name: "premium" },
    ];
    $scope.myRole = [];

    DataService.getQueue().success(function (data) {
        $scope.queue = data;

        for (var i = 0; i < $scope.queue.length; i++) {
            for (var j = 0; j < $scope.roles.length; j++) {
                if ($scope.nisht[i].Role == $scope.roles[j].value) {
                    $scope.myRole[i] = $scope.roles[j];
                }
            }

        }
    });

    $scope.deleteUser = function (data) {

        for (var i = 0; i < $scope.nisht.length; i++) {
            if ($scope.nisht[i].ID == data.ID) {
                $scope.nisht.splice(i, 1);
            }
        }
        DataService.delete(data.ID);
    }

    $scope.changeRoleUser = function (data, role) {
        DataService.changeStat(data.ID, role.value)
    }

});