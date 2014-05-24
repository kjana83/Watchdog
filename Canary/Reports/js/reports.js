var serviceModule = angular.module('Service', []);

var Model = function (http) {
    var self = this;

    this.Results = [];
    this.ServiceList = [];
    this.ServiceId = '1';
    this.FromDate = '';
    this.ToDate = '';
    this.GetReports = function () {
        http.get('/api/ServiceReports?ServiceId=' + self.ServiceId + '&FromDate=' + self.FromDate + '&ToDate=' + self.ToDate)
            .success(function (data, status, header, config) {
                self.Results = angular.fromJson(data);
            });
    };

    this.GetService = function () {
        http.get('/api/ServiceAdmin').success(function (data, status, headers, config) {
            self.ServiceList = angular.fromJson(data);
        }).error(function (data, status, headers, config) {
        });
    };
};

serviceModule.service('ServiceReport', ['$http', function (http) {
    var model = new Model(http);
    return model;
}]);

serviceModule.controller('ServiceController', ['ServiceReport', '$scope',
    function (service, $scope) {
        $scope.Model = service;
        $scope.Model.GetService();
    }]);

