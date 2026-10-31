using LatgaAutomatasLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasLibrary.Services.Interfaces
{
    public interface IBillService
    {
        void DefineBill(decimal denomination);

        Bill ExchangeToBill(decimal denomination);

    }
}
