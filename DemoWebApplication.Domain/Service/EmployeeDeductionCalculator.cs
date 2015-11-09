using System;
using System.Collections.Generic;
using System.Linq;
using DemoWebApplication.Domain.Model;

namespace DemoWebApplication.Domain.Service
{
    public class EmployeeDeductionCalculator
    {
        private readonly DeductionCalculatorSettings _calculatorSettings;

        public EmployeeDeductionCalculator(DeductionCalculatorSettings calculatorSettings)
        {
            _calculatorSettings = calculatorSettings;
        }

        public decimal CalculatePaycheckDeduction(Employee employee, params IDiscount<Person>[] discounts)
        {
            var allDiscounts = discounts.Any() ? discounts : _calculatorSettings.DefaultDiscounts;

            var salary = employee.AnnualSalary;

            var salaryMinusDeductions = salary - CalculateDiscounts(_calculatorSettings.EmployeeYearlyDeduction, employee, allDiscounts);

            if (employee.Dependents.Any())
            {
                foreach (var dependent in employee.Dependents)
                {
                    salaryMinusDeductions -= CalculateDiscounts(_calculatorSettings.DependentYearlyDeduction, dependent, allDiscounts);
                }
            }

            return Math.Round(salaryMinusDeductions / _calculatorSettings.PayCycles, 2);
        }

        private decimal CalculateDiscounts(decimal deduction, Person person, IEnumerable<IDiscount<Person>> discounts)
        {
            var approvedDiscounts = discounts.Where(r => r.DiscountRule(person));
            var deductionWithDiscounts = approvedDiscounts.Aggregate(deduction, (dep, dis) => dep - (dis.DiscountAmount * deduction));
            return deductionWithDiscounts;
        }
    }
}
