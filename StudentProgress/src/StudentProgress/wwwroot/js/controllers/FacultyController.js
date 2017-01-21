(function () {
    'use strict';
    angular
        .module('StudentProgressApp')
        .controller('FacultyController', FacultyController);

    FacultyController.$inject = ['$q', '$scope', '$http', 'FacultyService', 'AlertService', 'JournalSheetService'];

    function FacultyController($q, $scope, $http, FacultyService, AlertService, JournalSheetService) {

        $scope.createFaculty = function(faculty) {
            var facultyRequest = faculty.Name;
            $scope.faculty = '';

            $scope.createFacultyPromise = createCreateFacultyPromise(facultyRequest);
            $scope.createFacultyPromise.then(function() {
                $scope.getFaculties();
                AlertService.showSuccess("Successfuly");
                $scope.faculty.Name = "";
            });
        };

        var createCreateFacultyPromise = function(facultyRequest) {
            return $q(function(resolve, reject) {
                FacultyService
                    .createFaculty(facultyRequest)
                    .success(function(account) {
                        resolve(account);
                    }).error(function(response) {
                        console.error("createFaculty wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getFaculties = function() {
            $scope.getFacultiesPromise = createGetFacultiesPromise();
            $scope.getFacultiesPromise.then(function(faculty) {
                $scope.facultets = faculty;
            });
        };

        var createGetFacultiesPromise = function() {
            return $q(function(resolve, reject) {
                FacultyService
                    .getFaculties()
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("getFaculty wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.deleteFaculty = function(facultyId) {
            $scope.deleteFacultyPromise = createDeleteFacultysPromise(facultyId);
            $scope.deleteFacultyPromise.then(function() {
                $scope.getFaculties();
                AlertService.showSuccess("Successfuly");
            });
        };

        var createDeleteFacultysPromise = function(facultyId) {
            return $q(function(resolve, reject) {
                FacultyService
                    .deleteFaculty(facultyId)
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("deleteFaculty wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.updateFaculty = function(facultet) {
            $scope.updateFacultyPromise = createUpdateFacultysPromise(facultet);
            $scope.updateFacultyPromise.then(function() {
                $scope.getFaculties();
                AlertService.showSuccess("Successfuly");
            });
        };

        var createUpdateFacultysPromise = function(facultet) {
            return $q(function(resolve, reject) {
                FacultyService
                    .updateFaculty(facultet)
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("updateFaculty wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.createProfessor = function(professor, facultyId) {
            var professorRequest = {
                Name: professor.Name,
                SecondName: professor.Surname,
                Email: professor.Email,
                FacultyId: facultyId,
                Password: professor.Password
            };

            $scope.createProfessorPromise = createCreateProfessorPromise(professorRequest);
            $scope.createProfessorPromise.then(function() {
                AlertService.showSuccess("Successfuly");
                $scope.professor = null;
                $scope.facultet = "";
            });
        };

        var createCreateProfessorPromise = function(professorRequest) {
            return $q(function(resolve, reject) {
                FacultyService
                    .createProfessor(professorRequest)
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("createFaculty wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getProfessorFromCurrentFaculty = function(facultyId) {
            $scope.getProfessorFromCurrentFacultyPromise = createGetProfessorFromCurrentFacultyPromise(facultyId);
            $scope.getProfessorFromCurrentFacultyPromise.then(function(professors) {
                $scope.professors = professors;
            });
        };

        var createGetProfessorFromCurrentFacultyPromise = function(facultyId) {
            return $q(function(resolve, reject) {
                FacultyService
                    .getProfessorFromCurrentFaculty(facultyId)
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("createGetStudentFromCurrentGroupPromise accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.updateFacultyOnProfessor = function (facultyId, professorId) {
            var updatedProfessor = {
                FacultyId: facultyId,
                ProfessorId: professorId
            }
            $scope.updateFacultyOnProfessorPromise = createUpdateFacultyOnProfessorPromise(updatedProfessor);
            $scope.updateFacultyOnProfessorPromise.then(function () {
                $scope.getFaculties();
                AlertService.showSuccess("Successfuly");
            });
        };

        var createUpdateFacultyOnProfessorPromise = function(updatedProfessor) {
            return $q(function(resolve, reject) {
                FacultyService
                    .updateFacultyOnProfessor(updatedProfessor)
                    .success(function(data) {
                        resolve(data);
                    }).error(function(response) {
                        console.error("deleteProfessor accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.deleteProfessor = function(professorId, facultyId) {
            var removeRequest = {
                ProfessorId: professorId
            };

            $scope.deleteProfessorPromise = createDeleteProfessorPromise(removeRequest);
            $scope.deleteProfessorPromise.then(function() {
                $scope.getProfessorFromCurrentFaculty(facultyId);
                AlertService.showSuccess("Successfuly");
            });
        };

        var createDeleteProfessorPromise = function (removeRequest) {
            return $q(function (resolve, reject) {
                FacultyService
                    .deleteProfessor(removeRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("deleteProfessor accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getAllProfessors = function () {
            JournalSheetService
                   .getAllProfessors()
                   .success(function (professors) {
                       $scope.allProfessors = professors;
                   });
        };
    }
})();