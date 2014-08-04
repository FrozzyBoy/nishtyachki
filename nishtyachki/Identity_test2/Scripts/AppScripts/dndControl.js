myApp.controller('dndCntrl', function ($scope, $timeout) {
    $scope.list = ['one', 'two', 'three'];
    $scope.change = function () {
        console.log('darag or drop, i don`t know');
    }
});