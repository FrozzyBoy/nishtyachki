
myApp.controller("MyCtrl", function ($scope, DataService) {
    $scope.states = [{ value: 0, name: "free" },
        { value: 1, name: "locked" }
    ];
    $scope.myState = [];
    
    function getData() {
        DataService.getNisht().success(function (data) {
            $scope.nisht = data;
            if (data.length != 0) {
                $scope.count = (parseInt(data[data.length - 1].ID) + 1).toString();

                for (var i = 0; i < data.length; i++) {
                    data[i].Im = urls.bucket;
                }

                for (var i = 0; i < $scope.nisht.length; i++) {
                    for (var j = 0; j < $scope.states.length; j++) {
                        if ($scope.nisht[i].State == $scope.states[j].value) {
                            $scope.myState[i] = $scope.states[j];
                            break;
                        }
                        if (j == ($scope.states.length - 1)) {
                            $scope.myState[i] = { value: 2, name: "in using" };
                            $scope.nisht[i].Im = urls.bucketInUse;
                        }
                    }
                }
            }
            else {
                $scope.count = "1";
            }
        });
    }

    getData();

    queue.client.updateTable = function () {
        getData();
    }

    $scope.addNisht = function () {
        $scope.nisht.push({ Im: urls.bucket, ID: $scope.count, State: $scope.states[0].name });
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
                    $scope.nisht[i].Im = urls.bucket;
                }
                if (st.value == 1) {
                    $scope.nisht[i].Im = urls.bucketBlock;
                }
            }
        }
        DataService.changeStat(data.ID,st.value)
    }
    
});