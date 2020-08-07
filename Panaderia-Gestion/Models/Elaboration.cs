using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class Elaboration
    {
        public int elaboration_ID { get; set; }

        public int product_ID { get; set; }

        public double cantidad { get; set; }
    }
}