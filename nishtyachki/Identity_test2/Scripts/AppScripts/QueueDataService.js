myApp.service("QueueDataService", function ($http) {

    if (typeof sol == "undefined") {
        myValueToCountUrlsBecauseIESucs = 0;
        sol = function () {
            return "?" + ++myValueToCountUrlsBecauseIESucs;
        }
    }

    this.getQueue = function () {
        var url = urls.getWebApiControll("api/queue");
        return $http.get(url + sol());
    };
    this.delete = function (data) {
        var url = urls.getWebApiControll("api/queue/delete") + "/" + data;
        return $http.delete(url + sol());
    }
    this.changeStat = function (data, role) {
        var url = urls.getWebApiControll("api/queue/change");
        return $http.delete(url + "/" + data + "/role/" + role + sol());
    }
    this.blockUnblock = function () {
        var url = urls.getWebApiControll("api/queue/block");
        return $http.put(url + sol());
    }
    this.updateQueue = function (queue){
        var url = urls.getWebApiControll("api/queue/update/queue");
        return $http.post(url + sol(), queue);
    }
    this.sendMessage = function (msg, id) {
        var url = urls.getWebApiControll("api/queue/sendMsg");
        var data = [msg, id];
        return $http.post(url + sol(), data);
    }
});