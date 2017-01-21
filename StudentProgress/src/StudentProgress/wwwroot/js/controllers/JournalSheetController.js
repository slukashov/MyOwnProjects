(function () {
    'use strict';

    angular
        .module('StudentProgressApp')
        .controller('JournalSheetController', JournalSheetController);

    JournalSheetController.$inject = ['$q', '$scope', '$http', 'JournalSheetService', 'AlertService'];

    function JournalSheetController($q, $scope, $http, JournalSheetService, AlertService) {

        $scope.createLessonObject = function (id, mark, attending, date) {
            return {
                lessonId: null,
                mark: 0,
                attending: false,
                date: null
            };
        };

        $scope.hideShowSaveDateButton = function() {
            $scope.showSaveDate = false;
            $scope.showDatePickers = false;
            $scope.showPlusButton = true;
        };

        $scope.showOrHideEditButton = false;
        $scope.showInputAndSaveButton = false;
        $scope.showList = true;
        $scope.showDatePickers = false;
        $scope.showPlusButton = true;
        $scope.showSaveDate = false;
        $scope.showLabelWithDate = false;
        $scope.journalSheetAddSaveOrUpdate = true;
        $scope.journalSheetUpdateButton = false;
        $scope.journalSheetSaveButton = false;

        $scope.modelShowJournalSheet = {
            studentId: null,
            Name: null,
            Surname: null,
            Lessons: []
        }

        $scope.journalSheetAddSaveOrUpdate = true;
        $scope.journalSheetUpdateButton = false;
        $scope.journalSheetSaveButton = false;
        $scope.modelShowJournalSheet = {
            students: null,
            lessons: null,
            marks: new Array([]),
            attendings: new Array([])
        };

        $scope.modelOFStudentModels = null

        $scope.lessonAddButtonClick = function (id, mark, attending, date) {
            var lesson = $scope.createLessonObject(id, mark, attending, date);
            $scope.modelShowJournalSheet.Lessons.push(lesson);
        };

        $scope.modelFillFinalMarks = {
            marks: []
        };

        $scope.modelFinalMark = [];

        $scope.model = {
            group: null,
            discipline: null,
            professor: null,
            semester: null,
            journalSheet: null
        };

        $scope.dateModel = {
            date: null
        };

        $scope.showInputAndSaveButtonFunction = function () {
            $scope.showList = false;
            $scope.showInputAndSaveButton = true;
        };

        $scope.getAllStudentsFromCurrentGroup = function (groupId) {
            $scope.DropDownStatus = $scope.groupId;
            var groupRequest = groupId;
            JournalSheetService
                    .getAllStudentsFromCurrentGroup(groupRequest)
                    .success(function (students) {
                        $scope.modelShowJournalSheet.students = students;
                    });
            $scope.getAllJournalSheets(groupId);
        };

        $scope.getAllGroups = function () {
            JournalSheetService
                   .getAllGroups()
                   .success(function (groups) {
                       $scope.groups = groups;
                   });
        };

        $scope.getAllProfessors = function () {
            JournalSheetService
                   .getAllProfessors()
                   .success(function (professors) {
                       $scope.professors = professors;
                   });
        };

        $scope.createJournalSheet = function (groupId, disciplineId, professorId, semester) {
            var journalSheetRequest = {
                GroupId: groupId,
                DisciplineId: disciplineId,
                ProfessorId: professorId,
                Semester: semester
            };
            $scope.createJournalSheetPromise = createCreateJournalSheetPromise(journalSheetRequest);
            $scope.createJournalSheetPromise.then(function () {
                AlertService.showSuccess("Successfuly");
                $scope.model = null;
            });
        };

        var createCreateJournalSheetPromise = function (journalSheetRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .createJournalSheet(journalSheetRequest)
                           .success(function (disciplineRequest) {
                               resolve(disciplineRequest);
                           }).error(function (response) {
                               console.error("CreateJournalSheet wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.hideInputAndSaveButtonFunction = function (disciplineId, disciplineName) {
            $scope.showList = true;
            $scope.showInputAndSaveButton = false;
            var disciplineRequest = {
                Id: disciplineId,
                NewNameOfDiscipline: disciplineName
            };

            $scope.updateDiscipline(disciplineRequest);
        };

        $scope.updateDiscipline = function (disciplineRequest) {
            $scope.updateDisciplinePromise = createUpdateDisciplinePromise(disciplineRequest);
            $scope.updateDisciplinePromise.then(function () {
                $scope.getAllDisciplines();
                AlertService.showSuccess("Successfuly");
            });
        };

        var createUpdateDisciplinePromise = function (disciplineRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .updateDiscipline(disciplineRequest)
                    .success(function (disciplineRequest) {
                        resolve(disciplineRequest);
                    }).error(function (response) {
                        console.error("CreateJournalSheet wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.showButtonEdit = function () {
            $scope.showOrHideEditButton = true;
        };

        $scope.hideButtonEdit = function () {
            $scope.showOrHideEditButton = false;
        };

        $scope.createDiscipline = function (discipline) {
            var disciplineRequest = discipline.Name;
            $scope.discipline = '';
            $scope.createDisciplinePromise = createCreateDisciplinePromise(disciplineRequest);
            $scope.createDisciplinePromise.then(function () {
                $scope.getAllDisciplines();
                AlertService.showSuccess("Successfuly");
            });
        };

        var createCreateDisciplinePromise = function (disciplineRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .createDiscipline(disciplineRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getAllDisciplines = function () {
            $scope.getAllDisciplinesPromise = createGetAllDisciplinesPromise();
            $scope.getAllDisciplinesPromise.then(function (disciplines) {
                $scope.disciplines = disciplines;
            });
        };

        var createGetAllDisciplinesPromise = function () {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getAllDisciplines()
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.createLesson = function (journalSheetId, date, groupId) {
            var lessonRequest = {
                JournalSheetId: journalSheetId,
                Date: date,
                StudentModels: $scope.modelOFStudentModels

            };
            $scope.createLessonPromise = createCreateLessonPromise(lessonRequest);
            $scope.createLessonPromise.then(function () {
                $scope.getLessonsFromCurrentJournalSheet(groupId, journalSheetId);
            });
        };


        var createCreateLessonPromise = function (lessonRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .createLesson(lessonRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting createCreateLessonPromise wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getAllJournalSheets = function (groupId) {
            $scope.getAllJournalSheetsPromise = createGetAllJournalSheetsPromise(groupId);
            $scope.getAllJournalSheetsPromise.then(function (journalSheets) {
                $scope.journalSheets = journalSheets;
            });
        };

        var createGetAllJournalSheetsPromise = function (groupId) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getAllJournalSheets(groupId)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.showRow = false;

        $scope.getLessonsFromCurrentJournalSheet = function (groupId, journalSheetId) {
            var lessonRequest = {
                JournalSheetId: journalSheetId,
                GroupId: groupId
            };
            $scope.getLessonsFromCurrentJournalSheetPromise = createGetLessonFromCurrentJournalSheetPromise(lessonRequest);
            $scope.getLessonsFromCurrentJournalSheetPromise.then(function (modelOFStudentModels) {
                $scope.modelOFStudentModels = modelOFStudentModels;
            });
        };

        var createGetLessonFromCurrentJournalSheetPromise = function (lessonRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getLessonsFromCurrentJournalSheet(lessonRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("lessonRequest wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        var createGetAllJournalSheetsPromise = function (groupId) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getAllJournalSheets(groupId)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        var createGetLessonFromCurrentJournalSheetPromise = function (lessonRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getLessonsFromCurrentJournalSheet(lessonRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getFinalMarks = function (journalSheetId) {
            $scope.showRow = true;
            var finalMarkRequest = {
                Students: $scope.modelShowJournalSheet.students,
                JournalSheetId: journalSheetId
            };

            $scope.getFinalMarksPromise = createGetFinalMarksPromise(finalMarkRequest);
            $scope.getFinalMarksPromise.then(function (finalMarks) {
                $scope.modelFinalMark = finalMarks;
                if (finalMarks) {
                    $scope.journalSheetUpdateButton = true;
                    $scope.journalSheetSaveButton = true;
                }
                else {
                    $scope.journalSheetUpdateButton = false;
                    $scope.journalSheetSaveButton = true;
                }
            });
        };

        var createGetFinalMarksPromise = function (finalMarkRequest) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .getFinalMarks(finalMarkRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        AlertService.showError("FFUUUUUCCK");
                        reject();
                    });
            });
        };

        $scope.onTimeSet = function (newDate, oldDate) {
            $scope.model.date = newDate;
        };

        $scope.showDatePicker = function () {
            $scope.showDatePickers = true;
            $scope.showPlusButton = false;
            $scope.showSaveDate = true;
        };

        $scope.saveDate = function (journalSheetId, dateForDiscipline, groupId) {
            $scope.showPlusButton = true;
            $scope.showSaveDate = false;
            $scope.showLabelWithDate = true;
            $scope.showDatePickers = false;
            $scope.labelWithDate = dateForDiscipline;
            $scope.createLesson(journalSheetId, dateForDiscipline, groupId);
        };

        $scope.fillJournalSheet = function (journalSheetId) {
            var fillJournalSheet = {
                StudenModel: $scope.modelOFStudentModels
            };
            $scope.fillJournalSheetPromise = createFillJournalSheetPromise($scope.modelOFStudentModels);

            $scope.fillJournalSheetPromise = createFillJournalSheetPromise(fillJournalSheet);
            $scope.fillJournalSheetPromise.then(function () {
                AlertService.showSuccess("Successfuly");
            });
        };

        var createFillJournalSheetPromise = function (fillAttendingInJournalSheet) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .fillJournalSheet(fillAttendingInJournalSheet)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.confirmAttendingsInJournalSheet = function (journalSheetId) {
            var fillAttendingInJournalSheet = {
                JournalSheetId: $scope.modelShowJournalSheet.studentId,
                Lessons: $scope.modelShowJournalSheet.lessons
            };

            $scope.confirmAttendingsInJournalSheetPromise = createConfirmAttendingsInJournalSheetPromise(fillAttendingInJournalSheet);
            $scope.confirmAttendingsInJournalSheetPromise.then(function () {
                AlertService.showSuccess("Successfully");
            });
        };

        var createConfirmAttendingsInJournalSheetPromise = function (fillAttendingInJournalSheet) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .confirmAttendingsInJournalSheet(fillAttendingInJournalSheet)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        var createFillMarksInJournalSheetPromise = function (fillMarksInJournalSheet) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .fillMarksInJournalSheet(fillMarksInJournalSheet)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Someting wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };
        $scope.fillMarksInJournalSheet = function() {
            var fillMarksInJournalSheet = {
                ListOfStudents: $scope.modelShowJournalSheet.students,
                ListOfLessons: $scope.modelShowJournalSheet.lessons,
                ListOfMarks: $scope.modelShowJournalSheet.marks
            };
            $scope.fillMarksInJournalSheetPromise = createFillMarksInJournalSheetPromise(fillMarksInJournalSheet);
            $scope.fillMarksInJournalSheetPromise.then(function() {
                AlertService.showSuccess("Successfully");
            });
        }

        $scope.fillFinalMarks = function (journalSheetId, marks) {
            var fillFinalMarks = {
                JournalSheetId: journalSheetId,
                Students: $scope.modelShowJournalSheet.students,
                Ratings: marks
            };

            $scope.fillFinalMarksPromise = createFillFinalMarks(fillFinalMarks);
            $scope.fillFinalMarksPromise.then(function () {
                AlertService.showSuccess("Successfully");
            });
        };

        var createFillFinalMarks = function (fillFinalMarks) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .fillFinalMarks(fillFinalMarks)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Something in confirmAttendingsInJournalSheet wrong!");
                        console.error(response);
                        reject();
                    });
            });
        }

        $scope.updateFinalMarks = function () {
            $scope.updateFinalMarksPromise = createUpdateFinalMarksPromise($scope.modelFinalMark);
            $scope.updateFinalMarksPromise.then(function () {
                AlertService.showSuccess("Successfully");
            })
        };

        var createUpdateFinalMarksPromise = function (modelFinalMark) {
            return $q(function (resolve, reject) {
                JournalSheetService
                    .updateFinalMarks(modelFinalMark)
                    .success(function (data) {
                        resolve(data);
                    }).error(function (response) {
                        console.error("Something in confirmAttendingsInJournalSheet wrong!");
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.getMarks = function (journalSheetId) {
            var markRequest = {
                Students: $scope.modelShowJournalSheet.students,
                JournalSheetId: journalSheetId
            };
            JournalSheetService.getMarks(markRequest)
                .success(function (marks) {
                    $scope.modelMarks = marks;
                });
        };
    };
})();