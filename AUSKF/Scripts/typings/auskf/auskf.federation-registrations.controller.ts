// ReSharper disable once InconsistentNaming
module auskf {
    "use strict";

    interface IFederationRegistrationsScope extends ng.IScope {
        validationMessage: any;
        membershipList: Array<Event>; 
        dojoSelect: string;
        federationSelect: string;
        yearSelect: string;
        page: string;
    }

    export class FederationRegistrationsController {
        static $inject = ["$scope", "federationRegistrationsService"];

        constructor(private $scope: IFederationRegistrationsScope,
            private federationRegistrationsService: IFederationRegistrationsService) { 
            this.getRegisteredMembers($scope.page, $scope.federationSelect, $scope.dojoSelect, $scope.yearSelect); 
        }

        getRegisteredMembers(page, federationSelect, dojoSelect, yearSelect) {
            this.federationRegistrationsService.getRegisteredMembers(page, federationSelect, dojoSelect, yearSelect).success(data => {
                this.$scope.membershipList = data; 
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

    }

    angular.module("auskf")
        .controller("federationRegistrationsController", FederationRegistrationsController);
} 
 