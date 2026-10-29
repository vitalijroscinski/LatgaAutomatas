using System.Collections.Generic;

namespace LatgaAutomatasLibrary.Models
{
    public class SellOperation
    {
        public bool Success { get; set; } = false;
        public Dictionary<Bill, int> Rest { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
