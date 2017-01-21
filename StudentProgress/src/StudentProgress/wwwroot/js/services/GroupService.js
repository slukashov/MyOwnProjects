(function () {
    'use strict';

    angular
        .module('StudentProgressApp')
        .service('GroupService', GroupService);

    GroupService.$inject = ['$http'];

    function GroupService($http) {
        var url = "/api/Group";

        this.createGroup = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateGroup",
                data: JSON.stringify(data),
                headers: { 'Content-Type': 'application/json' }
            });
        };
    
        this.getGroup = function () {
            return $http.get(url + "/GetGroup");
        };

        this.createStudent = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateStudent",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getStudentFromCurrentGroup = function (data) {
            return $http({
                method: 'POST',
                url: url + "/GetStudentsFromCurrentGroup",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.createHeadman = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateHeadman",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.removeStudentFromCurrentGroup = function (data) {
            return $http({
                method: 'POST',
                url: url + "/RemoveStudentFromCurrentGroup",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();