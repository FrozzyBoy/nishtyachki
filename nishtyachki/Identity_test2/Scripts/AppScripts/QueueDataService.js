myApp.service("QueueDataService", function ($http) {

    if (typeof sol == "undefined") {
        myValueToCountUrlsBecauseIESucs = 0;
        sol = function () {
            var answer = typeof console == "undefined" ?
                ("?" + ++myValueToCountUrlsBecauseIESucs) : "";
            return answer;
        }
    }

    this.getQueue = function () {
        var url = urls.getWebApiControll("api/queue") + sol();
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = urls.getWebApiControll("api/queue/delete") + "/" + data + sol();
        return $http.get(url);
    }
    this.changeStat = function (data, role) {
        var url = urls.getWebApiControll("api/queue/change") + "/" + data + "/role/" + role + sol();
        return $http.get(url);
    }
    this.blockUnblock = function () {
        var url = urls.getWebApiControll("api/queue/block") + sol();
        return $http.get(url);
    }
    this.updateQueue = function (queue){
        var url = urls.getWebApiControll("api/queue/update/queue") + sol();
        return $http.get(url, queue);
    }
    this.sendMessage = function (msg, id) {
        var url = urls.getWebApiControll("api/queue/sendMsg") + sol();
        var data = [msg, id];
        return $http.get(url, data);
    }
});