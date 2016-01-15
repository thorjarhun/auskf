// ReSharper disable once InconsistentNaming
module auskf {
    "use strict";

    interface IDojoScope extends ng.IScope {
        validationMessage: any;
        dojolist: Array<Event>;
        stateslist: Array<Event>;
    }

    export class DojosController {
        static $inject = ["$scope", "dojosService"];

        constructor(private $scope: IDojoScope,
            private dojosService: IDojosService) {
            this.getDojos();
            this.getDojoStates();
        }

        getDojos() {
            this.dojosService.getDojos().success(data => {
                this.$scope.dojolist = data;
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




