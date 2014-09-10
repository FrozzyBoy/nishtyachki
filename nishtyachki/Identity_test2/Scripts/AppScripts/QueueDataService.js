myApp.service("QueueDataService", function ($http) {

    this.getQueue = function () {
        var url = urls.getWebApiControll("api/queue");
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = urls.getWebApiControll("api/queue/delete") + "/" + data;
        return $http.delete(url);
    }
    this.changeStat = function (data, role) {
        var url = urls.getWebApiControll("api/queue/change");
        return $http.delete(url + "/" + data + "/role/" + role);
    }
    this.blockUnblock = function () {
        var url = urls.getWebApiControll("api/queue/block");
        return $http.put(url);
    }
    this.updateQueue = function (queue){
        var url = urls.getWebApiControll("api/queue/update/queue");
        return $http.post(url, queue);
    }
    this.sendMessage = function (msg, id) {
        var url = urls.getWebApiControll("api/queue/sendMsg");
        var data = [msg, id];
        return $http.post(url, data);
    }
});