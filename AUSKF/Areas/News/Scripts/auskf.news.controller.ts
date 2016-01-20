// ReSharper disable once InconsistentNaming
alert("Test xyz");
 
module auskf {
    "use strict";

    interface INewsScope extends ng.IScope {
        validationMessage: any;
        news: Array<Event>;
    }

    export class NewsController {
        static $inject = ["$scope", "newsService"];

        constructor(protected $scope: INewsScope,
            private newsService: INewsService) { 
            alert("Test news controller constructor");
            this.getNews();
        }

        getNews() {
            alert("Test getNews controller");
            this.newsService.getNews().success(data => {
                this.$scope.news = data;
            }).error(error => {
                this.$scope.validationMessage = error.exceptionMessage;
            });
        }

    }

    angular.module("auskf")
        .controller("newsController", NewsController);
} 
 