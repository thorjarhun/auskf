// ReSharper disable once InconsistentNaming

module auskf {
    "use strict";

    export interface IFederationsService {
        getFederations(): ng.IHttpPromise<Array<Event>>; 
    }


    class FederationsService implements IFederationsService {
        serviceUri = "/api/v1/federations";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
        }

        getFederations(): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri);
        }  

    }

    function factory($http: ng.IHttpService, $q: ng.IQService): FederationsService {
        return new FederationsService($http, $q);
    }

    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("federationsService", FederationsService);
};
 