(function () {
    'use strict';

    angular
        .module('StudentProgressApp')
        .service('FacultyService', FacultyService);

    FacultyService.$inject = ['$http'];

    function FacultyService($http) {
        var url = "/api/Faculty";

        this.createFaculty = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateFaculty",
                data: JSON.stringify(data),
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getFaculties = function () {
            return $http.get(url + "/GetFaculties");
        };

        this.deleteFaculty = function (data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteFaculty",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.updateFaculty = function (data) {
            return $http({
                method: 'POST',
                url: url + "/UpdateFaculty",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.createProfessor = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateProfessor",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getProfessorFromCurrentFaculty = function (data) {
            return $http({
                method: 'POST',
                url: url + "/GetProfessorFromCurrentFaculty",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.updateFacultyOnProfessor = function(data) {
            return $http({
                method: 'POST',
                url: url + "/UpdateFacultyOnProfessor",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteProfessor = function(data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteProfessor",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();