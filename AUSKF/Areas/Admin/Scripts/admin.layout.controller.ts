// ReSharper disable once InconsistentNaming
module auskf.admin {
    "use strict";

    import userInfoViewModel = AUSKF.Domain.Models.Account.UserInfoViewModel;

    interface ILayoutScope extends ng.IScope {
        userMail: string;
        userModel: userInfoViewModel;
        validationMessage: string;
    }

    export class AdminLayoutController {
        static $inject = ["$scope", "$http", "$q"];

        serviceUri = "/api/v1/account/userInfo";

        constructor(private $scope: ILayoutScope, private $http: ng.IHttpService, private $q: ng.IQService) {
            this.getLoggedInUser();
        }

        getLoggedInUser(): any {
            this.$http.get(this.serviceUri).success(data => {
                this.$scope.userModel = <AUSKF.Domain.Models.Account.UserInfoViewModel>(data);
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });

        };
    }

    angular
        .module("auskf.admin")
        .controller("adminLayoutController", AdminLayoutController);
}