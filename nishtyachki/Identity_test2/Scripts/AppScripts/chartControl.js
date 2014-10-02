myApp.controller("chartControl", function ($scope, chartData) {

    $scope.data = true;

    $scope.drawtop = function (id, state) {
        window.onload = function () {
            var ctx = document.getElementById(id).getContext("2d");
            chartData.topusers(state).success(function (data) {

                var topData = {
                    labels: data.labels,
                    datasets: [
                        {
                            fillColor: "rgba(151,187,205,0.5)",
                            strokeColor: "rgba(151,187,205,0.8)",
                            highlightFill: "rgba(151,187,205,0.75)",
                            highlightStroke: "rgba(151,187,205,1)",
                            data: data.numbers
                        }]
                };

                window.myBar = new Chart(ctx).Bar(topData, {
                    responsive: true
                });
            }).error($scope.data = false);
            
        }
    }

    $scope.drawLine = function (element, id) {
        window.onload = function () {
            var ctx = document.getElementById(element).getContext("2d");
            chartData.everydayForUser(id).success(function (data) {

                var topData = {
                    labels: data.labels,
                    datasets: [
                        {
                            fillColor: "rgba(151,187,205,0.5)",
                            strokeColor: "rgba(151,187,205,0.8)",
                            highlightFill: "rgba(151,187,205,0.75)",
                            highlightStroke: "rgba(151,187,205,1)",
                            data: data.numbers
                        }]
                };

                window.myBar = new Chart(ctx).Bar(topData, {
                    responsive: true
                }).error($scope.data = false);
            })

        }
    }


});
