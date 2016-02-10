// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var FederationRegistrationsService = (function () {
        function FederationRegistrationsService($http, $q) {
            this.$http = $http;
            this.$q = $q;
            this.serviceUri = "/api/v1/federationmembership";
        }
        FederationRegistrationsService.prototype.getRegisteredMembers = function (page, federationId, dojoId, year) {
            return this.$http.get(this.serviceUri + "/paged/" + page + "? fedeationId = " + federationId + " & dojoId=" + dojoId + " & year=" + year);
        };
        return FederationRegistrationsService;
    })();
    function factory($http, $q) {
        return new FederationRegistrationsService($http, $q);
    }
    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("federationRegistrationsService", FederationRegistrationsService);
})(auskf || (auskf = {}));
;
//# sourceMappingURL=auskf.federation-registrations.service.js.map