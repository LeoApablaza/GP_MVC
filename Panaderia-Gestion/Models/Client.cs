using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class Client
    {
        [DisplayName("#")]
        public int client_ID { get; set; }

        [DisplayName("Nombre")]
        public string name { get; set; }

        [DisplayName("Telefono")]
        public long phone { get; set; }

        [DisplayName("Tiene deuda")]
        public bool debt { get; set; }
    }
}