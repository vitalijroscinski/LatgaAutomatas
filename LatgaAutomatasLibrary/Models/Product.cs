using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LatgaAutomatasLibrary.Models
{
    public record Product
    {
        public required string Name { get; init; }
        public required decimal Price { get; init; }
    }
}
