﻿myApp.controller("QueueCtrl", function ($scope, QueueDataService, signalrFctr) {
    var blockedButton = { Im: urls.getImg("red_button.jpg"), Message: "Queue is blocked!" };
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
            
        }).error(function (data, status, headers, config) {
           //alert("error recieve data!" + status);
        });
    };

    updateQueue();
    
    $scope.sortableOptions = {
        update: function (e, ui) {
            
            var newData = new Array($scope.queue.Queue.length);
            for (var i = 0; i < newData.length; i++) {
                newData[i] = $scope.queue.Queue[i].ID;
            }

            QueueDataService.updateQueue(newData);
        }
    };

    $scope.deleteUser = function (data) {
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
        QueueDataService.blockUnblock();       
    }

    $scope.sendMessage = function (msg, id) {

        this.elementId = id;
        var that = this;

        QueueDataService.sendMessage(msg, id).success(function (data, status) {
            document.getElementById(that.elementId).value = "";
        }).error(function (data, status) {
            //print error
        });
    }

    signalrFctr.initialize(updateQueue, 'queue');

});