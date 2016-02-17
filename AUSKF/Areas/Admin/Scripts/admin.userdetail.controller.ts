// ReSharper disable once InconsistentNaming
module auskf.admin {
    "use strict";

    import User = AUSKF.Domain.Entities.Identity.User;

    export interface IUserDetailScope extends ng.IScope {
        user: User;
        validationMessage: string;
    }

    export class AdminUserDetailController {
        static $inject = ["$scope", "$http", "$q", "coreService"];
        serviceUri = "/api/v1/admin/users/";

        constructor(
            
            private $scope: IUserDetailScope,
            private $http: ng.IHttpService,
            private $q: ng.IQService,
            protected coreService: ICoreService) {debugger;
            this.getUser();
        }

        getUser() {
            var id = this.coreService.getQueryStringParameter("id", true);
            this.$http.get(this.serviceUri + id)
                .success(data => {
                    this.$scope.user = <any>(data);
                }).error(error => {
                    this.$scope.validationMessage = error.exceptionMessage;
                });
        }
    }
    angular
        .module("auskf.admin")
        .controller("adminUserDetailController", AdminUserDetailController);
}  