using System;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DemoWebApplication.Domain;
using DemoWebApplication.Domain.Model;
using DemoWebApplication.Domain.Service;
using NUnit.Framework;

namespace DemoWebApplication.Tests
{
    [TestFixture]
    public class DemoWebApplicationTests
    {
        private DeductionCalculatorSettings _calculatorSettings = new DeductionCalculatorSettings();
        private EmployeeDeductionCalculator _deductionCalculator;

        [SetUp]
        public void SetupTests()
        {
            _deductionCalculator = new EmployeeDeductionCalculator(_calculatorSettings);
        }

        [Test]
        public void EmployeeCalculator_Employee_With_No_Dependents_Has_1000_Taken_In_Deductions()
        {
            var employee = new Employee();

            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee);

            //logic = (AnnualSalary - deduction) / 26
            //(($2000 * 26) - 1000) / 26 == 1961.54

            Assert.That(deduction, Is.EqualTo(1961.54));
        }

        [Test]
        public void EmployeeCalculator_Employee_With_1_Dependents_Has_1500_Taken_In_Deductions()
        {
            var employee = new Employee();

            employee.Dependents.Add(new Dependent());

            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee);

            //logic = (AnnualSalary - deduction) / 26
            //(($2000 * 26) - 1500) / 26 == 1942.31

            Assert.That(deduction, Is.EqualTo(1942.31));
        }

        [Test]
        public void EmployeeCalculator_Employee_With_3_Dependents_1_With_Name_Aaron_Has_2450_Taken_In_Deductions()
        {
            var employee = new Employee();
            employee.Dependents.AddRange(new[]
            {
                new Dependent() { FirstName = "Tommy", LastName = "Cole"},
                new Dependent() { FirstName = "Aaron", LastName = "Cole"},
                new Dependent() { FirstName = "Joey", LastName = "Cole"},
            });


            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee, new NameStartsWithLetterADiscount());

            //1000 - Employee
            //1000 - 500 x 2 Dependents
            //450 - Aaron ~ 500 * 0.9 = 450
            //2450 deduction total
            //(52000 - 2450) / 26 = 1905.77

            Assert.That(deduction, Is.EqualTo(1905.77));
        }

        [Test]
        public void EmployeeCalculator_Employee_With_3_Dependents_2_With_Name_Aaron_And_Arthur_Has_2350_Taken_In_Deductions()
        {
            var employee = new Employee() { FirstName = "Arthur", LastName = "Cole" };
            employee.Dependents.AddRange(new[]
            {
                new Dependent() { FirstName = "Tommy", LastName = "Cole"},
                new Dependent() { FirstName = "Aaron", LastName = "Cole"},
                new Dependent() { FirstName = "Joey", LastName = "Cole"},
            });


            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee, new NameStartsWithLetterADiscount());

            //900 - Employee (1000 - 100) == 900
            //1000 - 500 x 2 Dependents
            //450 - Aaron ~ 500 * 0.9 = 450
            //2350 deduction total
            //(52000 - 2350) / 26 = 1909.62

            Assert.That(deduction, Is.EqualTo(1909.62));
        }

        [Test]
        public void EmployeeCalculator_Employee_With_3_Dependents_2_With_Name_Aaron_And_Arthur_Has_2200_Taken_In_Deductions()
        {
            var employee = new Employee() { FirstName = "Arthur", LastName = "Cole" };
            employee.Dependents.AddRange(new[]
            {
                new Dependent() { FirstName = "Tommy", LastName = "Cole"},
                new Dependent() { FirstName = "Aaron", LastName = "Cole"},
                new Dependent() { FirstName = "Joey", LastName = "Cole"},
            });


            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee, new NameStartsWithLetterADiscount(0.2m));

            //use 20% instead of 10%
            //800 - Employee (1000 - 100) == 900
            //1000 - 500 x 2 Dependents
            //400 - Aaron ~ 500 * 0.9 = 450
            //2350 deduction total
            //(52000 - 2200) / 26 = 1909.62

            Assert.That(deduction, Is.EqualTo(1915.38));
        }

        [Test]
        public void EmployeeCalculator_Employee_With_3_Dependents_2_With_Name_Aaron_And_Arthur_Has_2200_Taken_In_Deductions_With_2_Discounts()
        {
            var employee = new Employee() { FirstName = "Arthur", LastName = "Cole" };
            employee.Dependents.AddRange(new[]
            {
                new Dependent() { FirstName = "Tommy", LastName = "Cole"},
                new Dependent() { FirstName = "Aaron", LastName = "Cole"},
                new Dependent() { FirstName = "Joey", LastName = "Cole"},
            });


            var deduction = _deductionCalculator.CalculatePaycheckDeduction(employee, new NameStartsWithLetterADiscount(),
                                                                                      new NameStartsWithLetterADiscount());            
            //800 - Employee (1000 - 100) == 900
            //1000 - 500 x 2 Dependents
            //400 - Aaron ~ 500 * 0.9 = 450
            //2350 deduction total
            //(52000 - 2200) / 26 = 1909.62

            Assert.That(deduction, Is.EqualTo(1915.38));
        }
    }
}
