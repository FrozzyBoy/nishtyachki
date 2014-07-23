myApp.controller("MyCtrl", function ($scope, DataService) {
    $scope.states = [{ value: 0, name: "free" },
        { value: 1, name: "locked" }
    ];
    $scope.myState = [];

    DataService.getNisht().success(function (data) {
        $scope.nisht = data;
        if (data.length != 0)
        {
            $scope.count = (parseInt(data[data.length - 1].ID) + 1).toString();

            for (var i = 0; i < $scope.nisht.length; i++) {
                for (var j = 0; j < $scope.states.length; j++) {
                    if ($scope.nisht[i].State == $scope.states[j].value) {
                        $scope.myState[i] = $scope.states[j];
                        break;
                    }
                    if (j == ($scope.states.length - 1)) {
                        $scope.myState[i] = { value: 2, name: "in using" };
                        $scope.nisht[i].Im = "/AdminApp/Resources/70299025_in_using.jpg";
                    }
                }
            }
        }
        else
        {
            $scope.count = "1";
        }

        
    });

    $scope.addNisht = function () {
        $scope.nisht.push({ Im: "/AdminApp/Resources/70299025.jpg", ID: $scope.count, State:$scope.states[0].name });
        $scope.myState.push($scope.states[0]);
        DataService.update($scope.count);
        $scope.count = (parseInt($scope.count) + 1).toString();
    }
    
    $scope.deleteNisht = function (data) {

        for (var i = 0; i < $scope.nisht.length; i++) {
            if ($scope.nisht[i].ID == data.ID)
            {
                $scope.nisht.splice(i, 1);
                $scope.myState.splice(i, 1);
            }
        }
        DataService.delete(data.ID);
    }

    $scope.changeStatNisht = function (data, st) {
        for (var i = 0; i < $scope.nisht.length; i++) {
            if ($scope.nisht[i].ID == data.ID) {
                $scope.nisht[i].State = st;
                if (st.value == 0) {
                    $scope.nisht[i].Im = "/AdminApp/Resources/70299025.jpg";
                }
                if (st.value == 1) {
                    $scope.nisht[i].Im = "/AdminApp/Resources/70299025_block.jpg";
                }
            }
        }
        DataService.changeStat(data.ID,st.value)
    }
    
});