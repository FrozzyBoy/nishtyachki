myApp.service("QueueDataService", function ($http) {

    this.getNisht = function () {
        var url = "http://localhost:60540/api/queue";
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = "http://localhost:60540/api/queue/delete";
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, role) {
        var url = "http://localhost:60540/api/queue/change";
        $http.delete(url + "/" + data + "/role/" + role);
    }
});