(function () {
    'use strict';

    angular
        .module('StudentProgressApp')
        .controller('GroupController', GroupController);

    GroupController.$inject = ['$q', '$scope', '$http', 'GroupService', 'AlertService'];

    function GroupController($q, $scope, $http, GroupService, AlertService) {
        $scope.edit = true;
        $scope.editInput = false;
        $scope.editButtonSave = false;
        $scope.showButtonsRemoveAndHeadman = false;
        
        $scope.selected = {
            student: null
        };

        $scope.selectStudent = function (student) {
            $scope.selected.student = student;
        };

        $scope.createGroup = function (group) {
            var groupRequest = group.Name;
            $scope.group = '';

            $scope.createGroupPromise = createCreateGroupPromise(groupRequest);
            $scope.createGroupPromise.then(function () {
                $scope.getGroup();
                AlertService.showSuccess("Successfuly");
                $scope.group.name = "";
            });
        };

        var createCreateGroupPromise = function (groupRequest) {
            return $q(function (resolve, reject) {
                GroupService
                    .createGroup(groupRequest)
                           .success(function (account) {
                               resolve(account);
                           }).error(function (response) {
                               console.error("createFaculty wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };


        $scope.getGroup = function () {
            $scope.getGroupPromise = createGetGroupPromise();
            $scope.getGroupPromise.then(function (groups) {
                $scope.groups = groups;
            });
        };

        var createGetGroupPromise = function () {
            return $q(function (resolve, reject) {
                GroupService
                    .getGroup()
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("getGroup accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.createStudent = function (student, groupId) {
            var studentRequest = {
                Name: student.Name,
                SecondName: student.SecondName,
                Email: student.Email,
                GroupId: groupId
            };

            $scope.createStudentPromise = createCreateStudentPromise(studentRequest);
            $scope.createStudentPromise.then(function () {
                AlertService.showSuccess("Successfuly");
                $scope.student = null;
                $scope.group = "";
            });
        };
        
        var createCreateStudentPromise = function (studentRequest) {
            return $q(function (resolve, reject) {
                GroupService
                    .createStudent(studentRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("creaeteGroup accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };
    
        $scope.getStudentFromCurrentGroup = function (groupId) {
            $scope.getStudentFromCurrentGroupPromise = createGetStudentFromCurrentGroupPromise(groupId);
            $scope.getStudentFromCurrentGroupPromise.then(function (students) {
                $scope.students = students;
            });
        };

        var createGetStudentFromCurrentGroupPromise = function (groupId) {
            return $q(function (resolve, reject) {
                GroupService
                    .getStudentFromCurrentGroup(groupId)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("createGetStudentFromCurrentGroupPromise accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.createHeadman = function (password, groupId, studentId, index) {

            $scope.editInput = false;
            $scope.editButton = false;
            $scope.showButtons = true;
            $scope.editButtonSave = false;

            var headmanRequest = {
                Password: password,
                StudentId: studentId,
                GroupId: groupId
            };

            $scope.createHeadmanPromise = createCreateHeadmanPromise(headmanRequest);
            $scope.createHeadmanPromise.then(function () {
                $scope.getStudentFromCurrentGroup(groupId);
                AlertService.showSuccess("Successfuly");
                $('#collapse_' + index).collapse("hide");
            });
        };

        var createCreateHeadmanPromise = function (headmanRequest) {
            return $q(function (resolve, reject) {
                GroupService
                    .createHeadman(headmanRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("creaeteHeadman accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.editButtonSaveFunciton = function () {
            $scope.edit = false;
            $scope.editInput = true;
            $scope.editButtonSave = true;
            $scope.showButtons = false;
        };

        $scope.removeStudentFromCurrentGroup = function (studentId, groupId) {
            var removeRequest = {
                StudentId: studentId
            };
            $scope.removeStudentFromCurrentGroup = createRemoveStudentFromCurrentGroupPromise(removeRequest);
            $scope.removeStudentFromCurrentGroup.then(function () {
                $scope.getStudentFromCurrentGroup(groupId);
                AlertService.showSuccess("Successfuly");
            });
        };

        var createRemoveStudentFromCurrentGroupPromise = function (removeRequest) {
            return $q(function (resolve, reject) {
                GroupService
                    .removeStudentFromCurrentGroup(removeRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("creaeteHeadman accounts error!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.showButtonRemoveAndHeadman = function () {
            $scope.showButtonsRemoveAndHeadman = true;
        };

        $scope.hideButtonRemoveAndHeadman = function () {
            $scope.showButtonsRemoveAndHeadman = false;
        };
    }
})();
