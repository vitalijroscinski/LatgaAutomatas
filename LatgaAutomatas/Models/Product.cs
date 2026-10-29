using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LatgaAutomatas.Models
{
    public class Product : IEqualityComparer<Product>
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }

        public bool Equals(Product? x, Product? y)
        {
            if (x == null || y == null)
                return false;

            return x.Name == y.Name;
        }

        public int GetHashCode(Product obj)
        {
            return Name.GetHashCode();
        }

        public override string ToString() => $"{Name} {Price}";
    }
}
