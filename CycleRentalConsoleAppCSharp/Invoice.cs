using System;
using System.Collections.Generic;
using System.Text;

namespace CycleRental
{
    public class Invoice
    {
        public List<string> Descriptions;
        public List<double> Amounts;

        public Invoice()
        {
            this.Amounts = new List<double>();
            this.Descriptions = new List<string>();
        }

        private double Sum()
        {
            double totalAmount = 0.0;
            for (int i = 0; i < this.Amounts.Count; i++)
            {
                totalAmount = totalAmount + this.Amounts[i];
            }

            return totalAmount;
        }

        public override string ToString()
        {
            var returnString = new StringBuilder();
            for (int i = 0; i < Descriptions.Count; i++)
            {
                returnString.Append(Descriptions[i]).Append(": ").Append(Amounts[i].ToString());
            }

            return string.Format("{0}\n-------------------------------------------\nTotal: {1}", returnString, this.Sum());
        }
    }
}
