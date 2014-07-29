myApp.service("QueueDataService", function ($http) {

    this.getQueue = function () {
        var url = urls.webApi().QueueDataService().getQueue;
        return $http.get(url);
    };
    this.delete = function (data) {
        var url = urls.webApi().QueueDataService().delete;
        $http.delete(url + "/" + data);
    }
    this.changeStat = function (data, role) {
        var url = urls.webApi().QueueDataService().changeStat;
        $http.delete(url + "/" + data + "/role/" + role);
    }
    this.blockUnblock = function () {
        var url = urls.webApi().QueueDataService().blockUnblock;
        $http.put(url);
    }
});