

// ReSharper disable once InconsistentNaming

module auskf {
    import Dojo = AUSKF.Domain.Entities.Dojo;
    import Tuple = System.Tuple;
    import SerializablePagination = AUSKF.Domain.Collections.SerializablePagination;
    "use strict";

    export interface IDojosService {
        getDojos(page): ng.IHttpPromise<SerializablePagination<Dojo>>;
        getDojosByState(page, stateSelect): ng.IHttpPromise<SerializablePagination<Dojo>>;
        getDojosByFederation(page, federationSelect): ng.IHttpPromise<SerializablePagination<Dojo>>;
        getDojoStates(): ng.IHttpPromise<Array<Tuple<string, string>>>;
    }


    class DojoService implements IDojosService {
        serviceUri = "/api/v1/dojos";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) { 
            this.getDojos(1);
        }

        getDojos(page): ng.IHttpPromise<SerializablePagination<Dojo>> {
            return this.$http.get(this.serviceUri + "/paged/" + page);
        } 

        getDojosByState(page, stateSelect): ng.IHttpPromise<SerializablePagination<Dojo>> {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=&state=" + stateSelect);
        }

        getDojosByFederation(page, federationSelect): ng.IHttpPromise<SerializablePagination<Dojo>> {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=" + federationSelect + "&state=");
        }

        getDojoStates(): ng.IHttpPromise<Array<Tuple<string, string>>> {
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

