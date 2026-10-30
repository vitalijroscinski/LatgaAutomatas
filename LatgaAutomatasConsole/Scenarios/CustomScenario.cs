using LatgaAutomatasConsole.Scenarios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasConsole.Scenarios
{
    internal class CustomScenario : IRunScenario
    {
        public void Run()
        {
            Console.WriteLine("Empty scenario. Example of usage find in any implemented IRunScenario type.");
        }
    }
}
