(function() {
    var person = function(firstName, lastName) {
        this.firstName = firstName || '';
        this.lastName = lastName || '';
        this.dependents = [];
    };

    person.prototype.isValid = function() {
        return this.firstName.length > 0 && this.lastName.length > 0;
    }; 

    window.models = window.models || {};

    window.models.Person = person;
}());




(function (ng, models) {

    var module = ng.module('DeductionCalculator', []);

    var controller = function($scope) {

        var calculatePaycheck = function() {
            //post the employee information back to the
            //api endpoint.  

            //returns a paycheck deduction amount

            $scope.paycheck = 2000 - 1000 - $scope.employee.dependents.length * 500;
        }

        $scope.employee = new models.Person();

        $scope.paycheck = 0;

        $scope.addEmployee = function (empl) {
            $.extend($scope.employee, empl);
            if ($scope.employee.isValid) {
                calculatePaycheck();
            }
        };

        $scope.addDependent = function (dep) {
            var dependent = $.extend(new models.Person(), dep);
            if (dependent.isValid()) {                
                $scope.employee.dependents.push(dependent);
                calculatePaycheck();
            }
        };

        $scope.clearForm = function() {
            $scope.employee = new models.Person();
        };
    };

    module.controller('employeeController', ['$scope', controller]);


}(window.angular, window.models));