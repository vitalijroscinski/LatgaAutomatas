using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LatgaAutomatasLibrary.Models
{
    public class Bill : IEqualityComparer<Bill>
    {
        public required string Name { get; set; }
        public required decimal Nominal { get; set; }

        public bool Equals(Bill? x, Bill? y)
        {
            if (x == null || y == null)
                return false;

            return x.Name == y.Name;
        }

        public int GetHashCode(Bill obj)
        {
            return Name.GetHashCode();
        }

        public override string ToString() => $"{Name} {Nominal}";
    }
}
