(function () {
    var person = function (firstName, lastName) {
        this.firstName = firstName || '';
        this.lastName = lastName || '';
        this.dependents = [];
    };

    person.prototype.isValid = function () {
        return this.firstName.length > 0 && this.lastName.length > 0;
    };

    window.models = window.models || {};

    window.models.Person = person;
}());




(function (ng, models) {

    var module = ng.module('DeductionCalculator', []);

    var controller = function ($scope) {

        var calculatePaycheck = function () {
            //post the employee information back to the
            //api endpoint.  
            //returns a paycheck deduction amount
            var payload = JSON.stringify($scope.employee);
            var calc = $.ajax('http://localhost:49785/api/employee/calculatePaycheck',
            {
                method: 'POST',
                data: payload,
                contentType: 'application/json'
            });

            calc.done(function (result) {
                $scope.$apply(function() {
                    $scope.paycheck = result;
                });
            });

        }

        $scope.employee = new models.Person();

        $scope.paycheck = 0;
        $scope.deduction = function () {
            return 2000 - $scope.paycheck;
        };

        $scope.addEmployee = function (empl) {
            $.extend($scope.employee, empl);
            if ($scope.employee.isValid()) {
                calculatePaycheck();
            }
        };

        $scope.addDependent = function (dep) {
            var dependent = $.extend(new models.Person(), dep);
            if (dependent.isValid()) {
                delete dependent.dependents; //remove, should use better OOP instead
                $scope.employee.dependents.push(dependent);
                calculatePaycheck();
                $scope.dep = null;
            }
        };

        $scope.removeDependent = function(dep) {
            var index = $scope.employee.dependents.indexOf(dep);
            $scope.employee.dependents.splice(index, 1);
            calculatePaycheck();
        }

        $scope.clearForm = function () {
            $scope.employee = new models.Person();
            $scope.empl = null;
        };
    };

    module.controller('employeeController', ['$scope', controller]);


}(window.angular, window.models));