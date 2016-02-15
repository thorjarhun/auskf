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
                this.$scope.searchValues = {
                    page: 1,
                    pageSize: 20,
                    orderBy: 'active, id',
                    sortDirection: 'ascending',
                    onlyShowActive: true
                };
                this.$scope.getClass = function (page, current) {
                    if (page === current) {
                        return "active";
                    }
                    return "";
                };
                this.$scope.getUsersBySearch = function () {
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
                this.$scope.getUsers = function (page, pageSize, sort, sortdirection) {
                    _this.$scope.searchValues.page = page;
                    _this.$scope.searchValues.pageSize = pageSize ? pageSize : 20;
                    _this.$scope.searchValues.orderBy = sort ? sort : "active, id";
                    _this.$scope.searchValues.sortDirection = sortdirection ? sortdirection : "ascending";
                    window.location.replace(_this.parseLocation());
                    _this.$http.get(_this.serviceUri + page +
                        "/?pagesize=" + _this.$scope.searchValues.pageSize +
                        "&sortdirection=" + _this.$scope.searchValues.sortDirection +
                        "&sortby=" + _this.$scope.searchValues.orderBy +
                        "&onlyShowActive=" + _this.$scope.searchValues.onlyShowActive +
                        "&query=" + _this.$scope.searchValues.query)
                        .success(function (data) {
                        _this.$scope.userList = (data);
                    }).error(function (error) {
                        _this.$scope.validationMessage = error.exceptionMessage;
                    });
                };
                this.$scope.getUsers(1, 20, 'id', 'ascending');
            }
            AdminUserController.prototype.parseLocation = function () {
                var location = "/Admin/User/#" + this.$scope.searchValues.page;
                location += this.$scope.searchValues.orderBy ?
                    "/?sortby=" + this.$scope.searchValues.orderBy : "";
                location += this.$scope.searchValues.pageSize ?
                    "&pagesize=" + this.$scope.searchValues.pageSize : "";
                location += this.$scope.searchValues.sortDirection ?
                    "&sortdirection=" + this.$scope.searchValues.sortDirection : "";
                return location;
            };
            AdminUserController.prototype.processAjaxData = function (response, urlPath) {
                document.getElementById("content").innerHTML = response.html;
                document.title = response.pageTitle;
                window.history.pushState({
                    "html": response.html,
                    "pageTitle": response.pageTitle
                }, "", urlPath);
            };
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