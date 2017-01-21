 (function () {
     'use strict';
     angular
         .module('StudentProgressApp')
         .service('JournalSheetService', JournalSheetService);
     JournalSheetService.$inject = ['$http'];

     function JournalSheetService($http) {
         var url = "/api/JournalSheet";

         this.createDiscipline = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/CreateDiscipline",
                 data: JSON.stringify(data),
                 headers: { 'Content-Type': 'application/json' }
             });
         };
        
         this.createLesson = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/CreateLesson",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.createJournalSheet = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/CreateJournalSheet",
                 data: JSON.stringify(data),
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.getAllDisciplines = function () {
             return $http.get(url + "/GetAllDisciplines");
         };

         this.getAllGroups = function () {
             return $http.get(url + "/GetAllGroups");
         };

         this.getAllProfessors = function () {
             return $http.get(url + "/GetAllProfessors");
         };

         this.getAllJournalSheets = function(data) {
             return $http({
                 method: 'POST',
                 url: url + "/GetAllJournalSheets",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };


         this.getAllStudentsFromCurrentGroup = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/GetAllStudentsFromCurrentGroup",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.getLessonsFromCurrentJournalSheet = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/GetLessonsFromCurrentJournalSheet",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.updateDiscipline = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/UpdateDiscipline",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.fillJournalSheet = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/FillAttendingInJournalSheet",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.fillMarksInJournalSheet = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/FillMarksInJournalSheet",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.confirmAttendingsInJournalSheet = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/ConfirmAttendingsInJournalSheet",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.fillFinalMarks = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/FillFinalMarks",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.getFinalMarks = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/GetFinalMarks",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.updateFinalMarks = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/UpdateFinalMarks",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };

         this.getMarks = function (data) {
             return $http({
                 method: 'POST',
                 url: url + "/GetMarks",
                 data: data,
                 headers: { 'Content-Type': 'application/json' }
             });
         };
     }
 })();