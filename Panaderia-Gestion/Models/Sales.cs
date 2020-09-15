using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class Sales
    {

        [DisplayName("#")]
        public int sale_ID { get; set; }

        public String date { get; set; }

       
        public double amount { get; set; }

        public int product_ID { get; set; }

        public int client_ID { get; set; }


       
        public string clientName { get; set; }

        public long clientPhone { get; set; }

        public bool clientDebt { get; set; }

        public string productName { get; set; }

        public decimal price { get; set; }

        public int wayToPay_ID { get; set; }

        public string wayToPay { get; set; }

        public int saleType_ID { get; set; }

        public string saleType { get; set; }


        [DisplayName("Ingreso")]
        public decimal total { get; set; }



    }
}