var app = angular.module('myApp', ['ngGrid']);
app.controller('MyCtrl', function($scope) {
    $scope.myData = [{name: 'Kyle Hayhurst'},
                     {name: 'Jimmy Hampton'},
                     {name: 'Tim Sweet'},
                     {name: 'Jonathon'},
                     {name: 'Brian Hann',}];
    $scope.gridOptions = { 
        data: 'myData',
        showGroupPanel: true,
        columnDefs: [{field: 'name', displayName: 'Name'},
            {displayName: 'Options', cellTemplate: '<input type="button" name="view" onclick="alert(\'You clicked view on item ID: {{row.entity.id}} \')" value="View">&nbsp;<input type="button" name="edit" onclick="alert(\'You clicked edit on item ID: {{row.entity.id}} \')" value="Edit">&nbsp;<input type="button" onclick="alert(\'You clicked delete on item ID: {{row.entity.id}} \')" name="delete" value="Delete">'}
            ]
    };
});