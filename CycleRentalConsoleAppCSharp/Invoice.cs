using System;
using System.Collections.Generic;
using System.Text;

namespace CycleRental
{
    public class Invoice
    {
        public List<String> descriptions;
        public List<double> amounts;

        public Invoice()
        {
            this.amounts = new List<double>();
            this.descriptions = new List<string>();
        }

        private double sum()
        {
            double totalAmount = 0.0;
            for (int i = 0; i < this.amounts.Count; i++)
            {
                totalAmount = totalAmount + this.amounts[i];
            }

            return totalAmount;
        }

        public override string ToString()
        {
            var returnString = new StringBuilder();
            for (int i = 0; i < descriptions.Count; i++)
            {
                returnString.Append(descriptions[i]).Append(": ").Append(amounts[i].ToString());
            }

            return string.Format("{0}\n-------------------------------------------\nTotal: {1}", returnString, this.sum());
        }
    }
}
