﻿myApp.service("DataService", function ($http) {

    this.getNisht = function () {
        var url = "http://localhost:60540/api/nisht";
        return $http.get(url);
    };

    this.update = function (id) {
        var url = "http://localhost:60540/api/nisht/add";
        $http.post(url+"/"+id);
    };

    this.delete = function (data) {
        var url = "http://localhost:60540/api/nisht/delete";
        $http.delete(url+"/"+data);
    }
    this.changeStat = function (data, state) {
        var url = "http://localhost:60540/api/nisht/change";
        $http.delete(url + "/" + data+"/state/"+state);
    }
});