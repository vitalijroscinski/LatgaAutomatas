using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasLibrary.Services
{
    public class BillService : IBillService
    {
        private List<Bill> _bills = new();
        public void DefineBill(string name, decimal nominal)
        {
            if (nominal <= 0)
                throw new Exception("Only positive nominals are accepted");

            if (_bills.Any(b => b.Nominal == nominal))
                throw new Exception("Bill already is defined");

            _bills.Add(new Bill { Name = name, Nominal = nominal });
        }

        public Bill ExchangeToBill(decimal nominal)
        {
            var bill = _bills.FirstOrDefault(b => b.Nominal == nominal);
            if (bill == null)
                throw new Exception("Bill was not found");

            return bill;
        }
    }
}
