namespace DemoWebApplication.Domain.Service
{
    public class DeductionCalculatorSettings
    {
        public decimal EmployeeYearlyDeduction { get; set; }
        public decimal DependentYearlyDeduction { get; set; }
        public int PayCycles { get; set; }

        public DeductionCalculatorSettings()
        {
            //default values
            EmployeeYearlyDeduction = 1000;
            DependentYearlyDeduction = 500;
            PayCycles = 26;
        }
    }
}