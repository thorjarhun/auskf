// ReSharper disable once InconsistentNaming
module auskf {
    "use strict";

    import Dojo = AUSKF.Domain.Entities.Dojo;
    import Tuple = System.Tuple;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;
    import SearchValues = AUSKF.Domain.Models.Account.SearchValues;

    export interface IDojoScope extends ng.IScope {
        validationMessage: any;
        dojoList: SerializablePagination<Dojo>;
        stateslist: Array<Tuple<string, string>>;
        federationSelect: string;
        stateSelect: string;
        searchValues: SearchValues;
    }

    export class DojosController {
        static $inject = ["$scope", "dojosService"];

        constructor(private $scope: IDojoScope,
            private dojosService: IDojosService) {
            this.getDojos($scope.searchValues.page); 
            this.getDojoStates();

            this.$scope.searchValues = <SearchValues>{
                page: 1,
                pageSize: 10,
                orderBy: 'dojoIId',
                sortDirection: 'ascending',
                onlyShowActive: true
            };
        }

        getDojos(page) {
            this.dojosService.getDojos(page).success(data => {
                this.$scope.dojoList = data;
                this.$scope.federationSelect = "";
                this.$scope.stateSelect = "";
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

        getDojosByState(page, stateSelect) {
            this.dojosService.getDojosByState(page, stateSelect).success(data => {
                this.$scope.dojoList = data;
                this.$scope.federationSelect = ""; 
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        } 

        getDojosByFederation(page, federationSelect) {
            this.dojosService.getDojosByFederation(page, federationSelect).success(data => {
                this.$scope.dojoList = data; 
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
 