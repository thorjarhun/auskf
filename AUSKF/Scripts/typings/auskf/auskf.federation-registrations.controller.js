// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var FederationRegistrationsController = (function () {
        function FederationRegistrationsController($scope, federationRegistrationsService) {
            this.$scope = $scope;
            this.federationRegistrationsService = federationRegistrationsService;
            this.getRegisteredMembers($scope.page, $scope.federationSelect, $scope.dojoSelect, $scope.yearSelect);
        }
        FederationRegistrationsController.prototype.getRegisteredMembers = function (page, federationSelect, dojoSelect, yearSelect) {
            var _this = this;
            this.federationRegistrationsService.getRegisteredMembers(page, federationSelect, dojoSelect, yearSelect).success(function (data) {
                _this.$scope.membershipList = data;
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        FederationRegistrationsController.$inject = ["$scope", "federationRegistrationsService"];
        return FederationRegistrationsController;
    })();
    auskf.FederationRegistrationsController = FederationRegistrationsController;
    angular.module("auskf")
        .controller("federationRegistrationsController", FederationRegistrationsController);
})(auskf || (auskf = {}));
//# sourceMappingURL=auskf.federation-registrations.controller.js.map