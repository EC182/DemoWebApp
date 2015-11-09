using System.Collections.Generic;

namespace DemoWebApplication.Domain.Model
{
    public class Employee : Person
    {
        public decimal AnnualSalary { get; set; }        
        public List<Dependent> Dependents { get; private set; }

        public Employee()
        {
            //default (26 * 2000)
            AnnualSalary = 52000m;
            Dependents = new List<Dependent>();
        }
    }
}