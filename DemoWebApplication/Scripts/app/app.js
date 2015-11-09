(function(ko) {

    var mainViewModel = function() {

        var employees = ko.observableArray([]);
        var selectedEmployee = ko.observable();

        var addDependent = function() {

        };

        var removeEmployee
    };

    var employeeViewModel = function() {

        var dependents = ko.observableArray([]);
        var firstName = ko.observable();
        var lastName = ko.observable();

    };

    var dependentViewModel = function() {

        var firstName = ko.observable();
        var lastName = ko.observable();
        
    };

    window.ViewModels = {
        MainViewModel: mainViewModel,
        EmployeeViewModel: employeeViewModel,
    };

}(window.ko));

(function(models, $, ko) {

    var pageVm = new models.MainViewModel();

    ko.applyBindings(pageVm);

}(window.ViewModels, window.jQuery, window.ko));