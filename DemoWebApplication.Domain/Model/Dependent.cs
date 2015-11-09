namespace DemoWebApplication.Domain.Model
{
    public class Dependent : Person
    {
        public bool IsSpouse { get; set; }
        public bool IsChild { get; set; }
    }
}