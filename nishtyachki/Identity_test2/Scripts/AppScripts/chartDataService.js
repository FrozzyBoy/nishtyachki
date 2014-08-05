myApp.service("chartData", function ($http) {
    this.topusers = function (count) {
        var url = urls.getWebApiControll("api/chartValues/" + count);
        return $http.get(url);
    }
});