using System;
using DemoWebApplication.Domain.Model;

namespace DemoWebApplication.Domain.Service
{
    public class NameStartsWithLetterADiscount : IDiscount<Person>
    {
        public Func<Person, bool> DiscountRule { get; protected set; }
        public decimal DiscountAmount { get; }

        public NameStartsWithLetterADiscount() : this(0.1m){ }

        public NameStartsWithLetterADiscount(decimal percentDiscount)
        {
            //Defaults
            DiscountAmount = percentDiscount;
            DiscountRule = (person) => !string.IsNullOrWhiteSpace(person.FullName) && person.FullName.StartsWith("A");
        }

    }
}