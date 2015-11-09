(function (ng) {

    var module = ng.module('DeductionCalculator', []);

    var controller = function($scope) {

        $scope.employee = {
            firstName: '',
            lastName: '',
            dependents : []
        };

        $scope.addEmployee = function() {
            
        };


        $scope.addDependent = function() {

        };
    };

    module.controller('employeeController', ['$scope', controller]);


}(window.angular));