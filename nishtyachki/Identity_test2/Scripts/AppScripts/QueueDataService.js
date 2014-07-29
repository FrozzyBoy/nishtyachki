myApp.service("QueueDataService", function ($http) {

    var control = "queue";

    this.getQueue = function () {
        var url = urls.getQueue(control);
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = urls.delete(control);
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, role) {
        var url = urls.changeStat(control);
        $http.delete(url + "/" + data + "/role/" + role);
    }
    this.blockUnblock = function () {
        var url = urls.blockUnblock(control);
        $http.put(url);
    }
});