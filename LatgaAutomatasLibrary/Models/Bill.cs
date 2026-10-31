using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LatgaAutomatasLibrary.Models
{
    public record Bill
    {
        public required decimal Nominal { get; init; }
    }
}
