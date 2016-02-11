var auskf;
(function (auskf) {
    var admin;
    (function (admin) {
        "use strict";
        var AdminUserController = (function () {
            function AdminUserController($scope, $http, $q) {
                var _this = this;
                this.$scope = $scope;
                this.$http = $http;
                this.$q = $q;
                this.serviceUri = "/api/v1/admin/users/";
                this.getUsers(1, 20, 'id');
                $scope.searchValues = {
                    page: 1,
                    pageSize: 20
                };
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
                        _this.$scope.searchValues.sortDirection = "Ascending";
                    }
                    if (!_this.$scope.searchValues.orderBy) {
                        _this.$scope.searchValues.orderBy = "auskfid";
                    }
                    if (!_this.$scope.searchValues.query) {
                        _this.$scope.searchValues.query = "";
                    }
                    _this.$http.get(_this.serviceUri +
                        "?page=" + _this.$scope.searchValues.page +
                        "&pagesize=" + _this.$scope.searchValues.pageSize +
                        "&sortdirection=" + _this.$scope.searchValues.sortDirection +
                        "&orderby=" + _this.$scope.searchValues.orderBy +
                        "&query=" + _this.$scope.searchValues.query).success(function (data) {
                        _this.$scope.userList = (data);
                    }).error(function (error) {
                        _this.$scope.validationMessage = error.exceptionMessage;
                    });
                };
                $scope.getUsers = function (page) {
                    _this.$scope.searchValues.page = page;
                    _this.$http.get(_this.serviceUri + page).success(function (data) {
                        _this.$scope.userList = (data);
                    }).error(function (error) {
                        _this.$scope.validationMessage = error.exceptionMessage;
                    });
                };
            }
            AdminUserController.prototype.getUsers = function (page, pageSize, sort) {
                var _this = this;
                this.$http.get(this.serviceUri + page + "/?sortby=" + sort).success(function (data) {
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