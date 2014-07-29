myApp.service("DataService", function ($http) {

    var control = "nisht";

    this.getNisht = function () {
        var url = urls.getNisht(control);
        return $http.get(url);
    };

    this.update = function (id) {
        var url = urls.update(control);
        $http.post(url + "/" + id);
    };

    this.delete = function (data) {
        var url = urls.delete(control);
        $http.delete(url + "/" + data);
    }

    this.changeStat = function (data, state) {
        var url = urls.changeStat(control);
        $http.delete(url + "/" + data + "/state/" + state);
    }
});