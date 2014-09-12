myApp.service("DataService", function ($http) {

    if (typeof sol == "undefined") {
        myValueToCountUrlsBecauseIESucs = 0;
        sol = function () {
            return "?" + ++myValueToCountUrlsBecauseIESucs;
        }
    }

    this.getNisht = function () {
        var url = urls.getWebApiControll("api/nisht");
        return $http.get(url + sol());
    };

    this.update = function (id) {
        var url = urls.getWebApiControll("api/nisht/add");
        $http.post(url + "/" + id + sol());
    };

    this.delete = function (data) {
        var url = urls.getWebApiControll("api/nisht/delete");
        $http.delete(url + "/" + data + sol());
    }

    this.changeStat = function (data, state) {
        var url = urls.getWebApiControll("api/nisht/change");
        $http.delete(url + "/" + data + "/state/" + state + sol());
    }
});