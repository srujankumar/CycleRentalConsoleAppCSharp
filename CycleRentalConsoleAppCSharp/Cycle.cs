using System;

namespace CycleRental
{
    public class Cycle
    {
        public int id;
        public string name;
        public string brand;
        public double basePrice;
        public double pricePerDay;
        public int noOfDays = 0;
        public bool isRented = false;
        public DateTime rentedOn;

        public Cycle(int id, string brand, string name, double pricePerDay)
        {
            this.id = id;
            this.brand = brand;
            this.name = name;
            this.pricePerDay = pricePerDay;
        }

        public Cycle(int id, string brand, string name, double basePrice, int noOfDays, double pricePerDay)
        {
            this.id = id;
            this.brand = brand;
            this.name = name;
            this.basePrice = basePrice;
            this.noOfDays = noOfDays;
            this.pricePerDay = pricePerDay;
        }

        public override string ToString()
        {
            string price;
            if (noOfDays != 0)
            {
                price = string.Format("{0} for {1} days. {2} after each day.", this.basePrice, this.noOfDays, this.pricePerDay);
            }
            else
            {
                price = string.Format("{0} per day", this.pricePerDay);
            }
            return string.Format("{0}|{1}|{2}|{3}", this.id, this.name, this.brand, price);
        }

        public override bool Equals(object cycle)
        {
            var value = (Cycle)cycle;
            return this.name.Equals(value.name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
