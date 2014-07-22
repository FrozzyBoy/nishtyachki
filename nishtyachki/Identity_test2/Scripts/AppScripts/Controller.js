myApp.controller("MyCtrl", function ($scope, DataService) {
    $scope.nisht = [];
    $scope.count = 1;
    $scope.states = [{ value: 0, name: "free" },
        { value: 1, name: "locked" },
        {value: 2, name: "in using"}
    ];
    $scope.myState = [];

    DataService.getNisht().success(function (data) {
        $scope.nisht = data;
        
        for (var i = 0; i < $scope.nisht.length; i++) {
            for (var j = 0; j < $scope.states.length; j++) {
                if ($scope.nisht[i].State == $scope.states[j].value) {
                    $scope.myState[i] = $scope.states[j];
                }
            }

        }
    });

    $scope.addNisht = function () {
        $scope.nisht.push({ Im: "/Resources/70299025.jpg", ID: $scope.count });
        $scope.myState.push($scope.states[0]);
        DataService.update($scope.count);
        $scope.count += 1;
    }
    
    $scope.deleteNisht = function (data) {

        for (var i = 0; i < $scope.nisht.length; i++) {
            if ($scope.nisht[i].ID == data.ID)
            {
                $scope.nisht.splice(i, 1);
            }
        }
        DataService.delete(data.ID);
    }

    $scope.changeStatNisht = function (data,st) {
        DataService.changeStat(data.ID,st.value)
    }
    
});