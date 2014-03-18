var adminModule = angular.module('Admin', []);

var ModelAdmin = function (http) {
    var self = this;
    this.Id = '';
    this.Name = '';
    this.Url = '';
    this.SoapAction = '';
    this.Method = 'Post';
    this.Request = '';
    this.Keyword = '';
    this.ContentType = 'application/json';
    this.CreateService = function () {
        var service = {
            Url: self.Url,
            Name: self.Name,
            SoapAction: self.SoapAction,
            Method: self.Method,
            Request: self.Request,
            Keyword: self.Keyword,
            ContentType: self.ContentType
        };
        http.post('/api/ServiceAdmin', service).success(function (data, status, headers, config) {
            toastr.info('Service created successfully');
            self.AddToList(service);
        }).error(function (data, status, headers, config) {
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

adminModule.service('AdminService', ['$http', function (http) {
    var model = new ModelAdmin(http);
    return model;
}]);

adminModule.controller('AdminController', ['AdminService', '$scope', function (adminService, $scope) {
    $scope.Model = adminService;
    $scope.Model.GetService();
}]);