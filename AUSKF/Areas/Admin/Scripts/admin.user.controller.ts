// ReSharper disable once InconsistentNaming
import SearchValues = AUSKF.Domain.Models.Account.SearchValues;

module auskf.admin {
    "use strict";

    import User = AUSKF.Domain.Entities.Identity.User;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;
    import SearchValues = AUSKF.Domain.Models.Account.SearchValues;
    import SortDirection = AUSKF.Domain.Interfaces.SortDirection;

    interface IUserScope extends ng.IScope {
        userList: SerializablePagination<User>;
        validationMessage: string;
        getUsersBySearch: Function;
        getClass: Function;
        searchValues: SearchValues;
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
                return "";
            };

            $scope.getUsersBySearch = () => {

                if (!this.$scope.searchValues.page) {
                    this.$scope.searchValues.page = 1;
                }

                if (!this.$scope.searchValues.sortDirection) {
                    this.$scope.searchValues.sortDirection = SortDirection.Ascending;
                }

                if (!this.$scope.searchValues.orderBy) {
                    this.$scope.searchValues.orderBy = "auskfid";
                }

                if (!this.$scope.searchValues.query) {
                    this.$scope.searchValues.query = "";
                }
                this.$http.get(this.serviceUri +
                    "?Page=" + this.$scope.searchValues.page +
                    "&PageSize=" + this.$scope.searchValues.pageSize +
                    "&SortDirection=" + this.$scope.searchValues.sortDirection +
                    "&OrderBy=" + this.$scope.searchValues.orderBy +
                    "&Query=" + this.$scope.searchValues.query
                    ).success(data => {
                        this.$scope.userList = <any>(data);
                    }).error(error => {
                        this.$scope.validationMessage = error.exceptionMessage;
                    });
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