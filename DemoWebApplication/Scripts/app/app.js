(function (ng) {

    var module = ng.module('DeductionCalculator', []);

    var controller = function($scope) {

        var calculateDeduction = function() {
            //post the employee information back to the
            //api endpoint.  

            //returns a paycheck deduction amount

            $scope.paycheckDeduction = 1000;
        }


        $scope.employee = {
            firstName: '',
            lastName : ''
        };

        $scope.paycheckDeduction = 0;

        $scope.addEmployee = function (empl) {            
            if (empl.firstName != null && empl.lastName != null) {
                $.extend($scope.employee, empl);
                $scope.employee.dependents = [];
                calculateDeduction();
            }
        };


        $scope.addDependent = function (dep) {
            if (dep.firstName != null && dep.lastName != null) {
                var newDep = $.extend({}, dep);
                $scope.employee.dependents.push(newDep);
            }
        };

        $scope.clearForm = function() {
            $scope.employee = {};
        };
    };

    module.controller('employeeController', ['$scope', controller]);


}(window.angular));