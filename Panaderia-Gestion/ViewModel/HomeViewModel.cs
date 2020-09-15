using Panaderia_Gestion.Models;
using Panaderia_Gestion.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.ViewModel
{
    public class HomeViewModel
    {
        Conexion cn = new Conexion();

        public Sales EarningsMonthly()
        {
            Sales sales = new Sales();

            SqlCommand cmd = new SqlCommand("SP_IngresosMensuales", cn.Connect());

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                sales.total = Convert.ToDecimal(dr["ingreso_mensual"]);
            }
            dr.Close(); cn.Disconnect();

            return sales;

        }
    }
}