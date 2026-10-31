using System.Collections.Generic;

namespace LatgaAutomatasLibrary.Models
{
    public record SellOperation
    {
        public bool Success { get; init; } = false;
        public Dictionary<Bill, int> Rest { get; init; } = new();
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
