﻿myApp.controller("chartControl", function ($scope, chartData) {

    $scope.data = true;
    $scope.noDataMsg = "";

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

                $scope.data = true;

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
                });

                $scope.data = true;

            }).error($scope.data = false);

        }
    }

    $scope.drawRadar = function (element, id) {
        window.onload = function () {
            var ctx = document.getElementById(element).getContext("2d");
            chartData.statisticForNishtiak(id).success(function (data) {

                var topData = {
                    labels: data.labels,
                    datasets: [
                        {
                            label: id + " nishtiak stats",
                            fillColor: "rgba(220,220,220,0.2)",
                            strokeColor: "rgba(220,220,220,1)",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: data.numbers
                        }]
                };

                window.myRadar = new Chart(ctx).Radar(topData, {
                    responsive: true
                });

                $scope.data = true;

            }).error($scope.data = false);
        }
    }

});
