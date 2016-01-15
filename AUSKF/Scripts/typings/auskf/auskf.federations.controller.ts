// ReSharper disable once InconsistentNaming
module auskf {
    "use strict";

    interface IFederationScope extends ng.IScope {
        validationMessage: any;
        federationlist: Array<Event>; 
    }

    export class FederationsController {
        static $inject = ["$scope", "federationsService"];

        constructor(private $scope: IFederationScope,
            private federationsService: IFederationsService) {
            this.getFederations(); 
        }

        getFederations() {
            this.federationsService.getFederations().success(data => {
                this.$scope.federationlist  = data;
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

    }

    angular.module("auskf")
        .controller("federationsController", FederationsController);
} 

