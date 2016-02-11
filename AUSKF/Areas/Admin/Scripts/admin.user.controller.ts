// ReSharper disable once InconsistentNaming
import SearchValues = AUSKF.Domain.Models.Account.SearchValues;

module auskf.admin {
    "use strict";

    import User = AUSKF.Domain.Entities.Identity.User;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;
    import SearchValues = AUSKF.Domain.Models.Account.SearchValues;


    interface IUserScope extends ng.IScope {
        userList: SerializablePagination<User>;
        validationMessage: string;
        getUsersBySearch: Function;
        getClass: Function;
        getUsers: Function;
        searchValues: SearchValues;
    }

    export class AdminUserController {
        static $inject = ["$scope", "$http", "$q"];
        serviceUri = "/api/v1/admin/users/";

        constructor(
            private $scope: IUserScope,
            private $http: ng.IHttpService,
            private $q: ng.IQService) {

            this.getUsers(1, 20, 'id');

            $scope.searchValues = <SearchValues>{
                page: 1,
                pageSize: 20
            };

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
                    this.$scope.searchValues.sortDirection = "Ascending";
                }

                if (!this.$scope.searchValues.orderBy) {
                    this.$scope.searchValues.orderBy = "auskfid";
                }

                if (!this.$scope.searchValues.query) {
                    this.$scope.searchValues.query = "";
                }
                this.$http.get(this.serviceUri +
                    "?page=" + this.$scope.searchValues.page +
                    "&pagesize=" + this.$scope.searchValues.pageSize +
                    "&sortdirection=" + this.$scope.searchValues.sortDirection +
                    "&orderby=" + this.$scope.searchValues.orderBy +
                    "&query=" + this.$scope.searchValues.query
                    ).success(data => {
                        this.$scope.userList = <any>(data);
                    }).error(error => {
                        this.$scope.validationMessage = error.exceptionMessage;
                    });
            };

            $scope.getUsers = (page: number) => {

                this.$scope.searchValues.page = page;

                this.$http.get(this.serviceUri + page).success(data => {
                    this.$scope.userList = <any>(data);

                }).error(error => {
                    this.$scope.validationMessage = error.exceptionMessage;

                });
            };
        }

        getUsers(page: number, pageSize: number, sort: string): any {
            this.$http.get(this.serviceUri + page + "/?sortby=" + sort).success(data => {
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