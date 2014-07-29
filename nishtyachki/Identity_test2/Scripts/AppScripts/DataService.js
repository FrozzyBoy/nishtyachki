myApp.service("DataService", function ($http) {

    this.getNisht = function () {
        var url = urls.webApi().DataService().getNisht;
        return $http.get(url);
    };

    this.update = function (id) {
        var url = urls.webApi().DataService().update;
        $http.post(url + "/" + id);
    };

    this.delete = function (data) {
        var url = urls.webApi().DataService().delete;
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, state) {
        var url = urls.webApi().DataService().changeStat;
        $http.delete(url + "/" + data + "/state/" + state);
    }
});