using System;

namespace CycleRental
{
    public class Customer
    {
        public string name;
        public int id;

        public Customer(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
