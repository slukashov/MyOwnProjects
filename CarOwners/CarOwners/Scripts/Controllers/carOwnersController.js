(function() {
    'use strict';

    angular
        .module('carOwnersAndTheirsCars', [])
        .controller('carOwnersController', carOwnersController);

    carOwnersController.$inject = ['$q', '$scope', '$http'];

    function carOwnersController($q, $scope, $http) {

        $scope.click = function () {
            
        };
    }
})();