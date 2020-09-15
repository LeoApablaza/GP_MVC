using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class History
    {
        public int history_ID { get; set; }

        public DateTime date { get; set; }

        public string description { get; set; }

        public string productName { get; set; }

        public int product_ID { get; set; }
    }
}