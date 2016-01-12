

// ReSharper disable once InconsistentNaming

module auskf {
    "use strict";
    export interface INewsService {
        getNews(): ng.IHttpPromise<Array<Event>>;
    }


    class NewsService implements INewsService {
        serviceUri = "/api/v1/event";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
        }

        getNews(): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri);
        }
    }

    function factory($http: ng.IHttpService, $q: ng.IQService): NewsService {
        return new NewsService($http, $q);
    }

    factory.$inject = ["$http", "$q"];
    angular
        .module("auskf")
        .service("newsService", NewsService);
};  