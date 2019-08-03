using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CycleRental;

namespace CycleRentalConsoleAppCSharp
{
    class Program
    {
        static void Main(string[] args)
    {
            Console.WriteLine("Press any key to exit.");
            List<Cycle> cycles = new List<Cycle>();
            cycles.Add(new Cycle(1, "BSA", "BSA SLR", 10.0));
            cycles.Add(new Cycle(2, "MDX", "Mountain Bike 101", 100.0, 10, 10.0));
            Dictionary<Cycle, Customer> rentals = new Dictionary<Cycle, Customer>();

            List<Customer> customers = new List<Customer>(); ;
            customers.Add(new Customer(1, "Kashi"));

            List<Invoice> invoices = new List<Invoice>();
            while (true)
            {
                string message = "Welcome to Cycle Rental!\n\n1. Search\n2. Rent\n3. Return\n4. Revenue\n5. Exit\n\nEnter your choice> ";
                int choice = int.Parse(prompt(message));
                switch (choice)
                {
                    case 1:
                        string brand = prompt("Enter the brand>");
                        List<Cycle> availableCycles = cycles.Where(c => !c.isRented).ToList();
                        List<Cycle> cyclesInBrand = availableCycles.Where(c => c.brand.ToLowerInvariant().Contains(brand.ToLowerInvariant())).ToList();
                        printCycles(cyclesInBrand);
                        break;
                    case 2:
                        int cycleId = int.Parse(prompt("Enter Cycle ID> "));
                        int customerId = int.Parse(prompt("Enter Customer ID> "));
                        Cycle cycle = cycles.FirstOrDefault(c => !c.isRented && c.id == cycleId);
                        Customer customer = customers.FirstOrDefault(c => c.id == customerId);
                        if (cycle != null)
                        {
                            rentals.Add(cycle, customer);
                            cycle.isRented = true;
                            cycle.rentedOn = new DateTime();
                        }

                        break;

                    case 3:
                        cycleId = int.Parse(prompt("Enter Cycle ID> "));
                        cycle = cycles.FirstOrDefault(c => c.isRented && c.id == cycleId);
                        if (cycle != null)
                        {
                            rentals.Remove(cycle);
                            cycle.isRented = false;
                            invoices.Add(getPrice(cycle));
                        }

                        Console.WriteLine(invoices[invoices.Count - 1]);
                        break;

                    case 4:
                        printInvoices(invoices);
                        Console.WriteLine(netAmount(invoices));
                        break;

                    case 5:
                        Console.WriteLine("Thank you");
                        return;
                    default:
                        throw new InvalidDataException();
                }
            }
        }

        public static void printCycles(List<Cycle> cycles)
        {
            foreach (var cycle in cycles)
            {
                Console.WriteLine(cycle.ToString());
            }
        }

        public static string prompt(string message)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            return input;
        }

        public static Invoice getPrice(Cycle cycle)
        {
            DateTime now = new DateTime();
            var timeSpan = cycle.rentedOn.Subtract(now);
            long totalDays = timeSpan.Days;
            if (cycle.noOfDays == 0)
            {
                Invoice invoice = new Invoice();
                invoice.descriptions.Add(string.Format("{0} Rent", totalDays));
                invoice.amounts.Add(totalDays * cycle.pricePerDay);
                return invoice;
            }
            else
            {
                Invoice invoice = new Invoice();
                invoice.descriptions.Add(string.Format("Base Rent for {0}", cycle.noOfDays));
                invoice.amounts.Add(cycle.basePrice);
                if (totalDays > cycle.noOfDays)
                {
                    long extraDays = totalDays - cycle.noOfDays;
                    double extraRent = extraDays * cycle.pricePerDay;
                    invoice.descriptions.Add(string.Format("Extra Rent for {0} extra days", cycle.noOfDays));
                    invoice.amounts.Add(extraRent);
                }
                return invoice;
            }
        }
        private static void printInvoices(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                Console.WriteLine(invoice.ToString());
                Console.WriteLine("=========================================");
            }
        }
        public static double netAmount(List<Invoice> invoices)
        {
            double totalSum = 0.0;
            foreach (var invoice in invoices)
            {
                foreach (var amount in invoice.amounts)
                {
                    totalSum += amount;
                }
            }
            return totalSum;
        }
    }
}
