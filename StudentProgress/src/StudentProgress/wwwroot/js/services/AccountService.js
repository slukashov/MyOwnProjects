(function () {
    'use strict';

    angular
        .module('StudentProgressApp')
        .service('AccountService', AccountService);

    AccountService.$inject = ['$http'];

    function AccountService($http) {
        var url = "/api/Account";

        this.getAccounts = function () {
            return $http.get(url + "/GetAccounts");
        };

        this.saveAccount = function (data) {
            return $http({
                method: 'POST',
                url: url + "/SaveUser", // save aacount or user?
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.confirmEmail = function () {
            return $http.get(url + "/ConfirmEmail");
        };
    }
})();