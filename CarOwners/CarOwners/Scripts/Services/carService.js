(function () {
    'use strict';

    angular
        .module('carOwnersAndTheirsCars')
        .service('carService', carService);

    carService.$inject = ['$http'];

    function carService($http) {

    }
})();