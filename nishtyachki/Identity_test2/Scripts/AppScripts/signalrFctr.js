myApp.factory('signalrFctr', function ($, $rootScope) {
    return {
        proxy:null,
        initialize: function (callback, hubName) {

            proxy = $.connection;
            
            proxy.hub.start();

            proxy[hubName].client.update = function () {
                $rootScope.$apply(callback());
            }

        }
    }
})