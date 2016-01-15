// ReSharper disable once InconsistentNaming
module auskf {
    "use strict";

    interface IDojoScope extends ng.IScope {
        validationMessage: any;
        dojolist: Array<Event>;
        stateslist: Array<Event>;
        federationSelect: string;
        stateSelect: string;
        page: string;
    }

    export class DojosController {
        static $inject = ["$scope", "dojosService"];

        constructor(private $scope: IDojoScope,
            private dojosService: IDojosService) {
            this.getDojos($scope.page);
            this.getDojosByState($scope.page, $scope.stateSelect);
            this.getDojosByFederation($scope.page, $scope.federationSelect);
            this.getDojoStates();
        }

        getDojos(page) {
            this.dojosService.getDojos(page).success(data => {
                this.$scope.dojolist = data;
                this.$scope.federationSelect = "";
                this.$scope.stateSelect = "";
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

        getDojosByState(page, stateSelect) {
            this.dojosService.getDojosByState(page, stateSelect).success(data => {
                this.$scope.dojolist = data;
                this.$scope.federationSelect = ""; 
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

        getDojosByFederation(page, federationSelect) {
            this.dojosService.getDojosByFederation(page, federationSelect).success(data => {
                this.$scope.dojolist = data; 
                this.$scope.stateSelect = "";
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

        getDojoStates() {
            this.dojosService.getDojoStates().success(data => {
                this.$scope.stateslist = data;
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        }

    } 

    angular.module("auskf")
        .controller("dojosController", DojosController);
} 
 