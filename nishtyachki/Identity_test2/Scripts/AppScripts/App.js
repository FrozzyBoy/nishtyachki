var queue = $.connection.queue;

queue.client.update = function () {
    console.log('new user');
}

$.connection.hub.start().done(function () {
    console.log("done");
    queue.server.test();
});

var myApp = angular.module("myApp", []);