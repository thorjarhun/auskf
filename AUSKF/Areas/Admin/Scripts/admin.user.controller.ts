// ReSharper disable once InconsistentNaming
module auskf.admin {
    "use strict";

    import User = AUSKF.Domain.Entities.Identity.User;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;

    interface IUserScope extends ng.IScope {
        userList: SerializablePagination<User>;
        validationMessage: string;
        getClass: Function;
    }

    export class AdminUserController {
        static $inject = ["$scope", "$http", "$q"];
        //https://localhost:44300/api/v1/admin/user/1/id
        serviceUri = "/api/v1/admin/user/";

        constructor(private $scope: IUserScope, private $http: ng.IHttpService, private $q: ng.IQService) {
            this.getUsers(1, 20, 'id');

            $scope.getClass = (page: number, current: number) => {
                if (page === current) {
                    return "active";
                }
            };
        }

        getUsers(page: number, pageSize: number, sort: string): any {
            this.$http.get(this.serviceUri + page + "/" + sort).success(data => {
                this.$scope.userList = <any>(data);
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        };

    }

    angular
        .module("auskf.admin")
        .controller("adminUserController", AdminUserController);
} 