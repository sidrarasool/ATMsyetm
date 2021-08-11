using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_ATM
{
   public class Customer
    {
        public string CustomerLoginID { get; set; }
        public int CustomerPinCode { get; set; }
        public string Holders_name { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
        public int AccountNO { get; set; }

        static int NoOfWrongInputs = 0;

    }
}
