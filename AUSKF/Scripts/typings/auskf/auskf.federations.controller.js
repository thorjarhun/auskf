// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var FederationsController = (function () {
        function FederationsController($scope, federationsService) {
            this.$scope = $scope;
            this.federationsService = federationsService;
            this.getFederations();
        }
        FederationsController.prototype.getFederations = function () {
            var _this = this;
            this.federationsService.getFederations().success(function (data) {
                _this.$scope.federationlist = data;
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        FederationsController.$inject = ["$scope", "federationsService"];
        return FederationsController;
    })();
    auskf.FederationsController = FederationsController;
    angular.module("auskf")
        .controller("federationsController", FederationsController);
})(auskf || (auskf = {}));
//# sourceMappingURL=auskf.federations.controller.js.map