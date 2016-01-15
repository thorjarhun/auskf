

// ReSharper disable once InconsistentNaming

module auskf {
    "use strict";

    export interface IDojosService {
        getDojos(page): ng.IHttpPromise<Array<Event>>;
        getDojosByState(page, stateSelect): ng.IHttpPromise<Array<Event>>;
        getDojosByFederation(page, federationSelect): ng.IHttpPromise<Array<Event>>;
        getDojoStates(): ng.IHttpPromise<Array<Event>>;
    }


    class DojoService implements IDojosService {
        serviceUri = "/api/v1/dojos";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
        }

        getDojos(page): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri + "/paged/" + page);
        } 

        getDojosByState(page, stateSelect): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=&state=" + stateSelect);
        }

        getDojosByFederation(page, federationSelect): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=" + federationSelect + "&state=");
        }

        getDojoStates(): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri + "/states");
        }  
    }

    function factory($http: ng.IHttpService, $q: ng.IQService): DojoService {
        return new DojoService($http, $q);
    }

    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("dojosService", DojoService);
}; 

