(function () {
    'use strict';

    angular
        .module('carOwnersAndTheirsCars')
        .service('carOwnersService', carOwnersService);

    carOwnersService.$inject = ['$http'];

    function carOwnersService($http) {

    }
})();