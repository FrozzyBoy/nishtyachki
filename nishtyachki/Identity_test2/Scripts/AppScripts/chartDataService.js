myApp.service("chartData", function ($http) {
    this.topusers = function (count) {
        var url = urls.getWebApiControll("api/chartValues/" + count);
        return $http.get(url);
    }

    this.everydayForUser = function (id) {
        var url = urls.getWebApiControll("api/chartValues/user/" + id);
        return $http.get(url);
    }
});