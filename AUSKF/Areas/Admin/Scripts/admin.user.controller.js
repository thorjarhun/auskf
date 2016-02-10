var auskf;
(function (auskf) {
    var admin;
    (function (admin) {
        "use strict";
        var SortDirection = AUSKF.Domain.Interfaces.SortDirection;
        var AdminUserController = (function () {
            function AdminUserController($scope, $http, $q) {
                var _this = this;
                this.$scope = $scope;
                this.$http = $http;
                this.$q = $q;
                //https://localhost:44300/api/v1/admin/user/1/id
                this.serviceUri = "/api/v1/admin/user/";
                this.getUsers(1, 20, 'id');
                $scope.getClass = function (page, current) {
                    if (page === current) {
                        return "active";
                    }
                    return "";
                };
                $scope.getUsersBySearch = function () {
                    if (!_this.$scope.searchValues.page) {
                        _this.$scope.searchValues.page = 1;
                    }
                    if (!_this.$scope.searchValues.sortDirection) {
                        _this.$scope.searchValues.sortDirection = SortDirection.Ascending;
                    }
                    if (!_this.$scope.searchValues.orderBy) {
                        _this.$scope.searchValues.orderBy = "auskfid";
                    }
                    if (!_this.$scope.searchValues.query) {
                        _this.$scope.searchValues.query = "";
                    }
                    _this.$http.get(_this.serviceUri +
                        "?Page=" + _this.$scope.searchValues.page +
                        "&PageSize=" + _this.$scope.searchValues.pageSize +
                        "&SortDirection=" + _this.$scope.searchValues.sortDirection +
                        "&OrderBy=" + _this.$scope.searchValues.orderBy +
                        "&Query=" + _this.$scope.searchValues.query).success(function (data) {
                        _this.$scope.userList = (data);
                    }).error(function (error) {
                        _this.$scope.validationMessage = error.exceptionMessage;
                    });
                };
            }
            AdminUserController.prototype.getUsers = function (page, pageSize, sort) {
                var _this = this;
                this.$http.get(this.serviceUri + page + "/" + sort).success(function (data) {
                    _this.$scope.userList = (data);
                }).error(function (error) {
                    _this.$scope.validationMessage = error.exceptionMessage;
                });
            };
            ;
            AdminUserController.$inject = ["$scope", "$http", "$q"];
            return AdminUserController;
        })();
        admin.AdminUserController = AdminUserController;
        angular
            .module("auskf.admin")
            .controller("adminUserController", AdminUserController);
    })(admin = auskf.admin || (auskf.admin = {}));
})(auskf || (auskf = {}));
//# sourceMappingURL=admin.user.controller.js.map