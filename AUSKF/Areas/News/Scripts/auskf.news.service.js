alert("Test abc");
// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var NewsService = (function () {
        function NewsService($http, $q) {
            this.$http = $http;
            this.$q = $q;
            this.serviceUri = "/api/v1/event";
            alert("Test news service constructor");
        }
        NewsService.prototype.getNews = function () {
            alert("Test service getNews");
            return this.$http.get(this.serviceUri);
        };
        return NewsService;
    })();
    function factory($http, $q) {
        return new NewsService($http, $q);
    }
    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("newsService", NewsService);
})(auskf || (auskf = {}));
;
//# sourceMappingURL=auskf.news.service.js.map