(function () {
    'use strict';
    angular
        .module('StudentProgressApp', ['toaster', 'ngAnimate', 'cgBusy']).value('cgBusyDefaults', createBusyDefaults());


    function createBusyDefaults() {
        return {
            backdrop: true,
            delay: 300,
            minDuration: 700,
            templateUrl: "templates/angularBusyTemplate.html"
        };
    }
})();