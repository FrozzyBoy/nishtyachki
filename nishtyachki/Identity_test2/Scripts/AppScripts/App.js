
var queue1 = $.connection.queue;

queue1.client.update = function () {
    console.log('new user');
}

$.connection.hub.start().done(function () {
    console.log("done");
    queue1.server.test();
});


var myApp = angular.module("myApp", []);