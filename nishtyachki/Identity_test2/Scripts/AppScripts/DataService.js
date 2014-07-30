myApp.service("DataService", function ($http) {

    this.getNisht = function () {
        var url = urls.getWebApiControll("api/nisht");
        return $http.get(url);
    };

    this.update = function (id) {
        var url = urls.getWebApiControll("api/nisht/add");
        $http.post(url + "/" + id);
    };

    this.delete = function (data) {
        var url = urls.getWebApiControll("api/nisht/delete");
        $http.delete(url + "/" + data);
    }

    this.changeStat = function (data, state) {
        var url = urls.getWebApiControll("api/nisht/change");
        $http.delete(url + "/" + data + "/state/" + state);
    }
});