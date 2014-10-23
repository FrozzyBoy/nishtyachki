myApp.service("DataService", function ($http) {

    if (typeof sol == "undefined") {        
        myValueToCountUrlsBecauseIESucs = 0;
        sol = function () {
            var answer = typeof console == "undefined" ?
                ("?" + ++myValueToCountUrlsBecauseIESucs) : "";
            return answer;
        }        
    }

    this.getNisht = function () {
        var url = urls.getWebApiControll("api/nisht") + sol();
        return $http.get(url);
    };

    this.addNisht = function () {
        var url = urls.getWebApiControll("api/nisht/add") + sol();
        $http.post(url);
    };

    this.delete = function (data) {
        var url = urls.getWebApiControll("api/nisht/delete") + "/" + data + sol();
        $http.delete(url);
    }

    this.changeStat = function (data, state) {
        var url = urls.getWebApiControll("api/nisht/change") + "/" + data + "/state/" + state + sol();
        $http.put(url);
    }
});