myApp.service("usersDataService", function ($http) {
    this.getUsers = function () {
        var url = urls.getWebApiControll("api/UserInfo");
        return $http.get(url);
    }
});