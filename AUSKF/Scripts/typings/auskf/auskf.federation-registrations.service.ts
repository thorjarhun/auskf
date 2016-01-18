
// ReSharper disable once InconsistentNaming

module auskf {
    "use strict";

    export interface IFederationRegistrationsService {
        getRegisteredMembers(page, federationId, dojoId, year): ng.IHttpPromise<Array<Event>>; 
    }


    class FederationRegistrationsService implements IFederationRegistrationsService {
        serviceUri = "/api/v1/federationmembership";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
        }

        getRegisteredMembers(page, federationId, dojoId, year): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri + "/paged/" + page + "? fedeationId = " + federationId + " & dojoId=" + dojoId + " & year=" + year);
        }
 
    }

    function factory($http: ng.IHttpService, $q: ng.IQService): FederationRegistrationsService {
        return new FederationRegistrationsService($http, $q);
    }

    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("federationRegistrationsService", FederationRegistrationsService);
};  