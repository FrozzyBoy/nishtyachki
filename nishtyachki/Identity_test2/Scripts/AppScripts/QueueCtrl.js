﻿myApp.controller("QueueCtrl", function ($scope, QueueDataService, signalrFctr) {
    var blockedButton = { Im: urls.getImg("red_button.jpg"), Message: "Qeueu is blocked!" };
    var unblockedButton = { Im: urls.getImg("green_button.png"), Message: "Queue is available!" };

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

            if (data.QueueState == 0) {
                $scope.blockButton = unblockedButton;
            }
            if (data.QueueState == 1) {
                $scope.blockButton = blockedButton;
            }
            
        });
    };

    updateQueue();
    
    $scope.sortableOptions = {
        update: function (e, ui) {
            QueueDataService.updateQueue($scope.queue.Queue);
        }
    };

    $scope.deleteUser = function (data) {
        QueueDataService.delete(data.ID).success(function (data, status) {
            console.log(data);
        }).error(function (data, status) {
            console.log(data);
        });
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

    $scope.sendMessage = function (msg, id) {
        QueueDataService.sendMessage(msg, id).success(function (data, status) {
            console.log(data);
        }).error(function (data, status) {
            console.log(data);
        });
    }

    signalrFctr.initialize(updateQueue, 'queue');

});