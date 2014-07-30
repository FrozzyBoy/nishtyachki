myApp.service("QueueDataService", function ($http) {

    this.getQueue = function () {
        var url = urls.getWebApiControll("api/queue");
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = urls.getWebApiControll("api/delete");
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, role) {
        var url = urls.getWebApiControll("api/queue/change");
        $http.delete(url + "/" + data + "/role/" + role);
    }
    this.blockUnblock = function () {
        var url = urls.getWebApiControll("api/queue/block");
        $http.put(url);
    }
});