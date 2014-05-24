var adminModule = angular.module('Admin', []);

var ModelAdmin = function (http, rootScope) {
    var self = this;
    this.Id = '';
    this.Name = '';
    this.Url = '';
    this.SoapAction = '';
    this.Method = 'Post';
    this.Request = '';
    this.Keyword = '';
    this.Response = '';
    this.Status = '';
    this.ContentType = 'application/json';
    this.CreateService = function () {
        var service = self.CollectService();
        http.post('/api/ServiceAdmin', service).success(function (data, status, headers, config) {
            toastr.info('Service created successfully');
            self.AddToList(service);
        }).error(function (data, status, headers, config) {
        });
    };

    this.CollectService = function () {
        return {
            Url: self.Url,
            Name: self.Name,
            SoapAction: self.SoapAction,
            Method: self.Method,
            Request: self.Request,
            Keyword: self.Keyword,
            ContentType: self.ContentType
        };
    };

    this.TestService = function (service) {
        service = service || self.CollectService();
        rootScope.service = {};
        http.post('/api/Service', service).success(function (data, status, header, config) {
            var result = angular.fromJson(data);
            rootScope.service.Response = result.Response;
            rootScope.service.Status = result.Status;
        }).error(function (data, status, header, config) {

        });
    };

    this.AddToList = function (service) {
        self.ServiceList.push(service);
        self.ClearAll();
    };

    this.ClearAll = function () {
        this.Id = '';
        this.Name = '';
        this.Url = '';
        this.SoapAction = '';
        this.Method = 'Post';
        this.Request = '';
        this.Keyword = '';
        this.ContentType = 'application/json';
    };

    this.ServiceList = [];
    this.GetService = function () {
        http.get('/api/ServiceAdmin').success(function (data, status, headers, config) {
            self.ServiceList = angular.fromJson(data);
        }).error(function (data, status, headers, config) {
        });
    };
};

adminModule.service('AdminService', ['$http','$rootScope', function (http,rootScope) {
    var model = new ModelAdmin(http, rootScope);
    return model;
}]);

adminModule.controller('AdminController', ['AdminService', '$scope', function (adminService, $scope) {
    $scope.Model = adminService;
    $scope.Model.GetService();
}]);