myApp.controller("QueueCtrl", function ($scope, QueueDataService) {
    $scope.roles = [{ value: 0, name: "standart" },
        { value: 1, name: "premium" },
    ];
    $scope.myRole = [];

    QueueDataService.getQueue().success(function (data) {
        $scope.queue = data;

        for (var i = 0; i < $scope.queue.length; i++) {
            for (var j = 0; j < $scope.roles.length; j++) {
                if ($scope.queue[i].Role == $scope.roles[j].value) {
                    $scope.myRole[i] = $scope.roles[j];
                }
            }

        }
    });

    $scope.deleteUser = function (data) {

        for (var i = 0; i < $scope.queue.length; i++) {
            if ($scope.queue[i].ID == data.ID) {
                $scope.queue.splice(i, 1);
            }
        }
        QueueDataService.delete(data.ID);
    }

    $scope.changeRoleUser = function (data, role) {
        for (var i = 0; i < $scope.queue.length; i++) {
            if ($scope.queue[i].ID == data.ID) {
                $scope.queue[i].Role = role.value;
            }
        }
        QueueDataService.changeStat(data.ID, role.value)
    }

});