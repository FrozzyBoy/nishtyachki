myApp.controller("QueueCtrl", function ($scope, QueueDataService) {
    var blockedButton = { Im: urls.buttonRed, Message: "Qeueu is blocked!" };
    var unblockedButton = { Im: urls.buttonGreen, Message: "Queue is available!" };

    $scope.roles = [{ value: 0, name: "standart" },
        { value: 1, name: "premium" },
    ];
    $scope.myRole = [];

    function updateQueue() {
        QueueDataService.getQueue().success(function (data) {
            $scope.queue = data;

            for (var i = 0; i < $scope.queue.Queue.length; i++) {
                for (var j = 0; j < $scope.roles.length; j++) {
                    if ($scope.queue.Queue[i].Role == $scope.roles[j].value) {
                        $scope.myRole[i] = $scope.roles[j];
                    }
                }

            }

            if (data._QueueState == 0) {
                $scope.blockButton = unblockedButton;
            }
            if (data._QueueState == 1) {
                $scope.blockButton = blockedButton;
            }
        });
    };

    updateQueue();

    queue.client.updateTable = function () {
        updateQueue();
        $scope.$apply();
    }

    $scope.deleteUser = function (data) {

        for (var i = 0; i < $scope.queue.Queue.length; i++) {
            if ($scope.queue.Queue[i].ID == data.ID) {
                $scope.queue.Queue.splice(i, 1);
            }
        }
        QueueDataService.delete(data.ID);
    }

    $scope.changeRoleUser = function (data, role) {
        for (var i = 0; i < $scope.queue.Queue.length; i++) {
            if ($scope.queue.Queue[i].ID == data.ID) {
                $scope.queue.Queue[i].Role = role.value;
            }
        }
        QueueDataService.changeStat(data.ID, role.value)
    }
    $scope.block = function (data) {
        if (data._QueueState == 1) {
            $scope.blockButton = unblockedButton;
            $scope.queue._QueueState = 0;
        }
        else  {
            $scope.blockButton = blockedButton;
            $scope.queue._QueueState = 1;
        }
        QueueDataService.blockUnblock();
    }

});