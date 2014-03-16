var serviceModule = angular.module('Service', []);

var Model = function(http) {
    var self = this;

    this.Results = [];

    this.ServiceList = [];
    this.GetService = function () {
        http.get('/api/ServiceAdmin').success(function (data, status, headers, config) {
            self.ServiceList = angular.fromJson(data);
        }).error(function (data, status, headers, config) {
        });
    };
    
    this.GetResults = function () {
        http.get('/api/ServiceResults').success(function(data, status, header, config) {
            self.Results = angular.fromJson(data);
            self.Map();
        });
    };

    this.Map = function() {
        _.each(self.Results, function(result) {
            var matchedService=_.find(self.ServiceList, function(service) {
                return result.ServiceId == service.Id;
            });
            result.Name = matchedService.Name;
            result.Url = matchedService.Url;
        });
    };
};

serviceModule.service('ServiceService', ['$http', function(http) {
    var model = new Model(http);
    return model;
}]);

serviceModule.controller('ServiceController', ['ServiceService', '$scope', '$timeout',
    function (service, $scope,$timeout) {
    $scope.Model = service;
    $scope.Model.GetService();
    var getResults = function() {
        $scope.Model.GetResults();
        $timeout(getResults, 60000);
    };
    getResults();
}]);