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
            protected coreService: ICoreService) {
            this.getUser();
        }

        getUser() {
            var id = this.coreService.getQueryStringParameter("userid", true);
            this.$http.get(this.serviceUri + id)
                .success(data => {
                    this.$scope.user = <any>(data);
                    if (this.$scope.user.dateOfBirth) {

                        var ds = new Date(this.$scope.user.dateOfBirth.toString());

                        this.$scope.user.displayDate =
                        ds.getFullYear().toString() + "-"
                        + ds.getMonth().toString() + "-"
                        + ds.getDay().toString();
                    }
                    this.$scope.user.promotions.forEach(p => {
                        var dds = new Date(p.rankDate.toString());
                        p.displayDate = dds.getFullYear().toString() + "-"
                        + dds.getMonth().toString() + "-"
                        + dds.getDay().toString();
                    });
                }).error(error => {
                    this.$scope.validationMessage = error.exceptionMessage;
                });
        }
    }
    angular
        .module("auskf.admin")
        .controller("adminUserDetailController", AdminUserDetailController);
}  