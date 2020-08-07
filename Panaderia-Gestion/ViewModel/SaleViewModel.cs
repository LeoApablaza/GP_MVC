using Panaderia_Gestion.Models;
using Panaderia_Gestion.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.ViewModel
{
    public class SaleViewModel
    {
        Conexion cn = new Conexion();

        //Lista forma de pago para el DropDownListFor del Create
        public IEnumerable<Sales> WayToPayListing()
        {
            Sales sales = new Sales();

            List<Sales> List = new List<Sales>();

            SqlCommand cmd = new SqlCommand("SP_ListarFormaPago", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                sales = new Sales();
                sales.wayToPay_ID = Convert.ToInt32(dr["id"]);
                sales.wayToPay = dr["nombre"].ToString();

                List.Add(sales);
            }

            dr.Close(); cn.Disconnect();

            return List;
        }
    }
}