// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    var admin;
    (function (admin) {
        "use strict";
        var AdminLayoutController = (function () {
            function AdminLayoutController($scope, $http, $q) {
                this.$scope = $scope;
                this.$http = $http;
                this.$q = $q;
                this.serviceUri = "/api/v1/account/userInfo";
                this.getLoggedInUser();
            }
            AdminLayoutController.prototype.getLoggedInUser = function () {
                var _this = this;
                this.$http.get(this.serviceUri).success(function (data) {
                    _this.$scope.userModel = (data);
                }).error(function (error) {
                    _this.$scope.validationMessage = error.exceptionMessage;
                });
            };
            ;
            AdminLayoutController.$inject = ["$scope", "$http", "$q"];
            return AdminLayoutController;
        })();
        admin.AdminLayoutController = AdminLayoutController;
        angular
            .module("auskf.admin")
            .controller("adminLayoutController", AdminLayoutController);
    })(admin = auskf.admin || (auskf.admin = {}));
})(auskf || (auskf = {}));
//# sourceMappingURL=admin.layout.controller.js.map