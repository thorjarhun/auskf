// ReSharper disable once InconsistentNaming
var auskf;
(function (auskf) {
    "use strict";
    var NewsController = (function () {
        function NewsController($scope, newsService) {
            this.$scope = $scope;
            this.newsService = newsService;
            this.getNews();
        }
        NewsController.prototype.getNews = function () {
            var _this = this;
            this.newsService.getNews().success(function (data) {
                _this.$scope.news = data;
            }).error(function (error) {
                _this.$scope.validationMessage = error.exceptionMessage;
            });
        };
        NewsController.$inject = ["$scope", "newsService"];
        return NewsController;
    })();
    auskf.NewsController = NewsController;
    angular.module("auskf")
        .controller("newsController", NewsController);
})(auskf || (auskf = {}));
//# sourceMappingURL=auskf.news.controller.js.map