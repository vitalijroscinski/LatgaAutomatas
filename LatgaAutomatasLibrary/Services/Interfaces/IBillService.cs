using LatgaAutomatasLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasLibrary.Services.Interfaces
{
    public interface IBillService
    {
        void DefineBill(string name, decimal denomination);

        Bill ExchangeToBill(decimal denomination);

    }
}
