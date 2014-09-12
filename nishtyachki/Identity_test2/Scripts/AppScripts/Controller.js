myApp.controller("MyCtrl", function ($scope, DataService, signalrFctr) {
    $scope.states = [{ value: 0, name: "free", imgUrl: urls.getImg("bucket.jpg") },
        { value: 1, name: "locked", imgUrl: urls.getImg("bucket_block.jpg") },
        { value: 2, name: "in use", imgUrl: urls.getImg("bucket_in_using.jpg") },
        { value: 3, name: "wait user answer", imgUrl: urls.getImg("bucket_wait_user.png") }
    ];
    $scope.myState = [];
    
    function getData() {
        DataService.getNisht().success(function (data) {
            /*
            alert("recieve data!");
            alert("length  " + data.length + " state " + data[0].State + " id " + data[0].ID);
            */
            $scope.nisht = data;
            if (data.length != 0) {
                $scope.count = data.length;

                for (var i = 0; i < data.length; i++) {                    
                    $scope.myState[i] = $scope.states[data[i].State];
                    $scope.nisht[i].Im = $scope.states[data[i].State].imgUrl;
                }
            }
        }).error(function (data, status, headers, config) {
            //alert("error recieve data!");
        });
    }

    getData();

    $scope.addNisht = function () {
        $scope.nisht.push({ID: $scope.count, State: $scope.states[0].name });
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
            }
        }
        DataService.changeStat(data.ID,st.value)
    }
    
    signalrFctr.initialize(getData, 'nishtiak');

});