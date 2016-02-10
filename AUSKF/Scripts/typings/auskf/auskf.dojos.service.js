// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var DojoService = (function () {
        function DojoService($http, $q) {
            this.$http = $http;
            this.$q = $q;
            this.serviceUri = "/api/v1/dojos";
            this.getDojos(1);
        }
        DojoService.prototype.getDojos = function (page) {
            return this.$http.get(this.serviceUri + "/paged/" + page);
        };
        DojoService.prototype.getDojosByState = function (page, stateSelect) {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=&state=" + stateSelect);
        };
        DojoService.prototype.getDojosByFederation = function (page, federationSelect) {
            return this.$http.get(this.serviceUri + "dojos/paged/" + page + "?federationId=" + federationSelect + "&state=");
        };
        DojoService.prototype.getDojoStates = function () {
            return this.$http.get(this.serviceUri + "/states");
        };
        return DojoService;
    })();
    function factory($http, $q) {
        return new DojoService($http, $q);
    }
    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("dojosService", DojoService);
})(auskf || (auskf = {}));
;
//# sourceMappingURL=auskf.dojos.service.js.map