// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var DojosController = (function () {
        function DojosController($scope, dojosService) {
            this.$scope = $scope;
            this.dojosService = dojosService;
            this.getDojos($scope.searchValues.page);
            this.getDojoStates();
            this.$scope.searchValues = {
                page: 1,
                pageSize: 10,
                orderBy: 'dojoIId',
                sortDirection: 'ascending',
                onlyShowActive: true
            };
        }
        DojosController.prototype.getDojos = function (page) {
            var _this = this;
            this.dojosService.getDojos(page).success(function (data) {
                _this.$scope.dojoList = data;
                _this.$scope.federationSelect = "";
                _this.$scope.stateSelect = "";
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        DojosController.prototype.getDojosByState = function (page, stateSelect) {
            var _this = this;
            this.dojosService.getDojosByState(page, stateSelect).success(function (data) {
                _this.$scope.dojoList = data;
                _this.$scope.federationSelect = "";
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        DojosController.prototype.getDojosByFederation = function (page, federationSelect) {
            var _this = this;
            this.dojosService.getDojosByFederation(page, federationSelect).success(function (data) {
                _this.$scope.dojoList = data;
                _this.$scope.stateSelect = "";
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        DojosController.prototype.getDojoStates = function () {
            var _this = this;
            this.dojosService.getDojoStates().success(function (data) {
                _this.$scope.stateslist = data;
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        DojosController.$inject = ["$scope", "dojosService"];
        return DojosController;
    })();
    auskf.DojosController = DojosController;
    angular.module("auskf")
        .controller("dojosController", DojosController);
})(auskf || (auskf = {}));
//# sourceMappingURL=auskf.dojos.controller.js.map