using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class Sales
    {
        public int sale_ID { get; set; }

        public double amount { get; set; }

        public int product_ID { get; set; }

        public int client_ID { get; set; }

        public int wayToPay_ID { get; set; }

        public string wayToPay { get; set; }

        public int saleType_ID { get; set; }

        public double total { get; set; }


    }
}