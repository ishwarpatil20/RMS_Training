debugger;

var app = angular.module('Domain', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('DomainCtrl', function ($scope, $http) {
    $scope.Domains = null;
    $scope.Domain_SubDomain = null;
    debugger;
    BindDomains();
    function BindDomains() {
        $scope.GetDomains = function () {
            $http.get('http://localhost:4260/Domain/GetDomains')
                .then(function (d) {
                    debugger;
                $scope.Domains = d.JsonData; // Success
                },
            function () {
                alert('Error Occured !!!'); // Failed
            });
        }

        $http.get("http://localhost:4260/readme.txt")
    .then(function (response) {
        debugger;
        $scope.myWelcome = response.data;
    });


    }

    function BindDomainSubDomain(DomainId,SubDomainId) {
        $scope.GetDomain_SubDomain = function () {
            $http.get('/Domain/GetDomains_SubDomain?DomainId=' + DomainId + '&SubDomainId=' + SubDomainId).then(function (d) {
                $scope.Domain_SubDomain = d.JsonData; // Success
            }, function () {
                alert('Error Occured !!!'); // Failed
            });
        }
    }

});