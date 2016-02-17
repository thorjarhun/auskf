// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    var admin;
    (function (admin) {
        "use strict";
        var AdminUserDetailController = (function () {
            function AdminUserDetailController($scope, $http, $q, coreService) {
                this.$scope = $scope;
                this.$http = $http;
                this.$q = $q;
                this.coreService = coreService;
                this.serviceUri = "/api/v1/admin/users/";
                this.getUser();
            }
            AdminUserDetailController.prototype.getUser = function () {
                var _this = this;
                var id = this.coreService.getQueryStringParameter("userid", true);
                this.$http.get(this.serviceUri + id)
                    .success(function (data) {
                    _this.$scope.user = (data);
                    if (_this.$scope.user.dateOfBirth) {
                        var ds = new Date(_this.$scope.user.dateOfBirth.toString());
                        _this.$scope.user.displayDate =
                            ds.getFullYear().toString() + "-"
                                + ds.getMonth().toString() + "-"
                                + ds.getDay().toString();
                    }
                    _this.$scope.user.promotions.forEach(function (p) {
                        var dds = new Date(p.rankDate.toString());
                        p.displayDate = dds.getFullYear().toString() + "-"
                            + dds.getMonth().toString() + "-"
                            + dds.getDay().toString();
                    });
                }).error(function (error) {
                    _this.$scope.validationMessage = error.exceptionMessage;
                });
            };
            AdminUserDetailController.$inject = ["$scope", "$http", "$q", "coreService"];
            return AdminUserDetailController;
        })();
        admin.AdminUserDetailController = AdminUserDetailController;
        angular
            .module("auskf.admin")
            .controller("adminUserDetailController", AdminUserDetailController);
    })(admin = auskf.admin || (auskf.admin = {}));
})(auskf || (auskf = {}));
//# sourceMappingURL=admin.userdetail.controller.js.map