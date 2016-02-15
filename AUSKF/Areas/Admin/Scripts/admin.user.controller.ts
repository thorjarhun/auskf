// ReSharper disable once InconsistentNaming
import SearchValues = AUSKF.Domain.Models.Account.SearchValues;

module auskf.admin {
    "use strict";

    import User = AUSKF.Domain.Entities.Identity.User;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;
    import SearchValues = AUSKF.Domain.Models.Account.SearchValues;


    export interface IUserScope extends ng.IScope {
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

            this.$scope.searchValues = <SearchValues>{
                page: 1,
                pageSize: 20,
                orderBy: 'active, id',
                sortDirection: 'ascending',
                onlyShowActive: true
            };

            this.$scope.getClass = (page: number, current: number) => {
                if (page === current) {
                    return "active";
                }
                return "";
            };

            this.$scope.getUsersBySearch = () => {

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

            this.$scope.getUsers = (page: number, pageSize: number, sort: string, sortdirection: string) => {

                this.$scope.searchValues.page = page;
                this.$scope.searchValues.pageSize = pageSize ? pageSize : 20;
                this.$scope.searchValues.orderBy = sort ? sort : "active, id";
                this.$scope.searchValues.sortDirection = sortdirection ? sortdirection : "ascending";

                window.location.replace(this.parseLocation());
                this.$http.get(this.serviceUri + page +
                    "/?pagesize=" + this.$scope.searchValues.pageSize +
                    "&sortdirection=" + this.$scope.searchValues.sortDirection +
                    "&sortby=" + this.$scope.searchValues.orderBy +
                    "&onlyShowActive=" + this.$scope.searchValues.onlyShowActive +
                    "&query=" + this.$scope.searchValues.query)
                    .success(data => {
                        this.$scope.userList = <any>(data);
                    }).error(error => {
                        this.$scope.validationMessage = error.exceptionMessage;
                    });
            }

            this.$scope.getUsers(1, 20, 'id', 'ascending');
        }

        parseLocation(): string {
            var location = "/Admin/User/#" + this.$scope.searchValues.page;

            location += this.$scope.searchValues.orderBy ?
                "/?sortby=" + this.$scope.searchValues.orderBy : "";

            location += this.$scope.searchValues.pageSize ?
                "&pagesize=" + this.$scope.searchValues.pageSize : "";

            location += this.$scope.searchValues.sortDirection ?
                "&sortdirection=" + this.$scope.searchValues.sortDirection : "";

            return location;
        }

        processAjaxData(response: any, urlPath: string): void {
            document.getElementById("content").innerHTML = response.html;
            document.title = response.pageTitle;
            window.history.pushState(
                {
                    "html": response.html,
                    "pageTitle": response.pageTitle
                },
                "",
                urlPath
                );
        }

    }

    angular
        .module("auskf.admin")
        .controller("adminUserController", AdminUserController);
} 