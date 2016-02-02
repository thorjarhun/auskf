// ReSharper disable once InconsistentNaming
module auskf.admin {
    "use strict";


    interface ILayoutScope extends ng.IScope {
    }

    export class AdminLayoutController {
        static $inject = ["$scope", "$http", "$q"];

        serviceUri = "/api/v1/account/userInfo";

        constructor(private $scope: ILayoutScope, private $http: ng.IHttpService, private $q: ng.IQService) {
            this.getLoggedInUser();
        }

        getLoggedInUser(): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri);
        };
    }

    angular
        .module("auskf.admin")
        .controller("adminLayoutController", AdminLayoutController);
}