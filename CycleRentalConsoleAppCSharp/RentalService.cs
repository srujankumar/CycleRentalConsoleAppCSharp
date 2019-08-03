using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CycleRental;

namespace CycleRentalConsoleAppCSharp
{
    public class RentalService
    {
        public Dictionary<int, Invoice> Invoices = new Dictionary<int, Invoice>();
        public Dictionary<Customer, Cycle> Rentals = new Dictionary<Customer, Cycle>();
        public List<Cycle> Cycles;
        public List<Customer> Customers;

        public RentalService(List<Cycle> cycles, List<Customer> customers)
        {
            Cycles = cycles;
            Customers = customers;
        }

        public void RentCycle(int cycleId, int customerId)
        {
            var cycle = Cycles.Find(eachCycle => !eachCycle.isRented && eachCycle.id == cycleId);
            var customer = Customers.Find(eachCustomer => eachCustomer.id == customerId);
            Rentals.Add(customer,cycle);
            cycle.isRented = true;
        }

        public string ReturnCycle(int customerId, int noOfDays)
        {
            var customer = Customers.Find(eachCustomer => eachCustomer.id == customerId);
            var cycle = Rentals[customer];
            Rentals.Remove(customer);
            cycle.isRented = false;
            //Invoice For minimum days.
            if (noOfDays <= cycle.noOfDays)
            {
                var invoice = new Invoice();
                invoice.Descriptions.Add($"{noOfDays} Rent");
                invoice.Amounts.Add(cycle.basePrice);
                return invoice.ToString();
            } else {
                var invoice = new Invoice();
                invoice.Descriptions.Add($"Base rent for {cycle.noOfDays}");
                invoice.Amounts.Add(cycle.basePrice);
                if (noOfDays > cycle.noOfDays)
                {
                    var extraDays = noOfDays - cycle.noOfDays;
                    var extraRent = extraDays * cycle.pricePerDay;
                    invoice.Descriptions.Add($"Extra rent for {extraDays} extra days");
                    invoice.Amounts.Add(extraRent);
                }
                return invoice.ToString();
            }
        }

        //Don't remove this method. It will use in future.
        private static string PrintInvoices(List<Invoice> invoices)
        {
            StringBuilder combinedInvoices = new StringBuilder();
            foreach (var invoice in invoices)
            {
                 combinedInvoices.Append(invoice.ToString());
            }

            return combinedInvoices.ToString();

        }
    }
}
