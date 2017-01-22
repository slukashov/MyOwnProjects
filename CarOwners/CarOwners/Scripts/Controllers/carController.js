(function () {
    'use strict';

    angular
        .module('carAndTheirsCars', [])
        .controller('carController', carController);

    carController.$inject = ['$q', '$scope', '$http'];

    function carController($q, $scope, $http) {

    }
})();