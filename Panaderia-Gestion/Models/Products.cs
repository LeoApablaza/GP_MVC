using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.Models
{
    public class Products
    {
        [DisplayName("#")]
        public int product_ID { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [DisplayName("Nombre")]
        public string name { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [DisplayName("Precio $")]
        public decimal price { get; set; }

        public int sale_type_ID { get; set; }


        [Required(ErrorMessage = "Eliga una forma de venta")]
        [DisplayName("Tipo de venta")]
        public string sale_type { get; set; }

        [DisplayName("Código de barra")]
        public string  bar_code { get; set; }

    }
}