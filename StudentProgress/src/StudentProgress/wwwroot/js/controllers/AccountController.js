(function () {
    'use strict';

    angular
     .module('StudentProgressApp')
     .controller('AccountController', AccountController);

    AccountController.$inject = ['$q', '$scope', '$http', 'AccountService', 'AlertService'];

    function AccountController($q, $scope, $http, AccountService, AlertService) {
        $scope.isInputsDisabled = true;
        $scope.isPasswordDisabled = true;
        $scope.showEdit = true;
        $scope.showSave = false;
        $scope.showPassword = false;
        $scope.oldPasswordAccount = false;
        $scope.newPasswordAccount = false;
        $scope.savePasswordButton = false;

        $scope.getAccounts = function () {
            $scope.getAccountsPromise = createGetAccountsPromise();
            $scope.getAccountsPromise.then(function (accounts) {
                $scope.accounts = accounts;
            });
        };

        var createGetAccountsPromise = function () {
            return $q(function (resolve, reject) {
                AccountService
                    .getAccounts()
                    .success(function (accounts) {
                        resolve(accounts);
                    }).error(function (response) {
                        console.error("Getting accounts error!")
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.selectAccount = function (account) {
            $scope.account = account;
        };

        $scope.edit = function () {
            $scope.showSave = true;
            $scope.showEdit = false;
            $scope.isInputsDisabled = false;
            $scope.showPassword = true;
        };

        $scope.saveAccount = function (account) {
            $scope.isInputsDisabled = true;
            $scope.showEdit = true;
            $scope.showSave = false;
            $scope.showPassword = false;
            $scope.saveAccountPromise = createSaveAccountPromise(account);
            $scope.saveAccountPromise.then(function () {
                $scope.getAccounts();
                AlertService.showSuccess("Successfuly");
                $scope.account = null;
                $scope.OldPassword = "";
                $scope.NewPassword = "";
            });
        };

        var createSaveAccountPromise = function (account) {
            return $q(function (resolve, reject) {
                AccountService
                    .saveAccount(account)
                    .success(function (account) {
                        resolve(account);
                    }).error(function (response) {
                        console.error("Saving accounts error!")
                        console.error(response);
                        reject();
                    });
            });
        };

        $scope.showInputs = function () {
            $scope.oldPasswordAccount = true;
            $scope.newPasswordAccount = true;
            $scope.savePasswordButton = true;
            $scope.showPassword = false;
            $scope.showSave = false;
        };

        $scope.saveNewPass = function (oldPassword, newPassword) {
            $scope.oldPasswordAccount = false;
            $scope.newPasswordAccount = false;
            $scope.savePasswordButton = false;
            $scope.showPassword = true;
            $scope.showSave = true;
            if (newPassword.length <= 5)
                alert("Password too short");
            else {
                if ($scope.account.Password == oldPassword)
                    $scope.account.Password = newPassword;
                else
                    alert("Wrong Old Password");
            }
            $scope.OldPassword = "";
            $scope.NewPassword = "";
        };
    }
})();