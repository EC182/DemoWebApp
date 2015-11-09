using System;

namespace DemoWebApplication.Domain
{
    public interface IDiscount<T>
    {
        Func<T, bool> DiscountRule { get; }
        decimal DiscountAmount { get; }
    }
}