// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var FederationsService = (function () {
        function FederationsService($http, $q) {
            this.$http = $http;
            this.$q = $q;
            this.serviceUri = "/api/v1/federations";
        }
        FederationsService.prototype.getFederations = function () {
            return this.$http.get(this.serviceUri);
        };
        return FederationsService;
    })();
    function factory($http, $q) {
        return new FederationsService($http, $q);
    }
    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("federationsService", FederationsService);
})(auskf || (auskf = {}));
;
//# sourceMappingURL=auskt.federations.service.js.map