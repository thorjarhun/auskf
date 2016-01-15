

// ReSharper disable once InconsistentNaming

module auskf {
    "use strict";

    export interface IDojosService {
        getDojos(): ng.IHttpPromise<Array<Event>>;
        getDojoStates(): ng.IHttpPromise<Array<Event>>;
    }


    class DojoService implements IDojosService {
        serviceUri = "/api/v1/dojos";

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
        }

        getDojos(): ng.IHttpPromise<Array<Event>> {
            return this.$http.get(this.serviceUri);
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
 






//$http.get(API_URL + 'dojos/paged/1').then(
//    function (success) {
//        $scope.dojolist = success.data.currentPage;
//        $scope.federationSelect = "";
//        $scope.stateSelect = "";
//    }, function (error) {
//        //alert('danger', 'Sorry   ',  error.data.message, 2000);
//    });

//$http.get(API_URL + 'federations').then(
//    function (success) {
//        $scope.federationlist = success.data;
//    }, function (error) {
//        alert(error.data.message);
//        //alert('danger', 'Sorry   ',  error.data.message, 2000);
//    });



//$scope.selectDojosByState = function () {
//    $scope.federationSelect = "";
//    $http.get(API_URL + 'dojos/paged/1?federationId=&state=' + $scope.stateSelect).then(
//        function (success) {
//            $scope.dojolist = success.data.currentPage;
//        }, function (error) {
//            //alert('danger', 'Sorry   ',  error.data.message, 2000);
//        });
//};

//$scope.selectDojoByFederation = function () {
//    $scope.stateSelect = "";
//    $http.get(API_URL + 'dojos/paged/1?federationId=' + $scope.federationSelect + '&state=').then(
//        function (success) {
//            $scope.dojolist = success.data.currentPage;
//        }, function (error) {
//            //alert('danger', 'Sorry   ',  error.data.message, 2000);
//        });
//};

