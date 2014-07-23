myApp.service("QueueDataService", function ($http) {

    this.getQueue = function () {
        var url = "http://localhost/AdminApp/api/queue";
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = "http://localhost/AdminApp/api/queue/delete";
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, role) {
        var url = "http://localhost/AdminApp/api/queue/change";
        $http.delete(url + "/" + data + "/role/" + role);
    }
    this.blockUnblock = function () {
        var url = "http://localhost/AdminApp/api/queue/block";
        $http.put(url);
    }
});