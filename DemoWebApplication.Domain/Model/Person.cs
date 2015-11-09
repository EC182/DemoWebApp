namespace DemoWebApplication.Domain.Model
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName ?? string.Empty, LastName ?? string.Empty); }
        }
    }
}