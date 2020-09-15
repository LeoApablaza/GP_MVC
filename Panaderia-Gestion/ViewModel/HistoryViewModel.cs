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
    public class HistoryViewModel
    {
        Conexion cn = new Conexion();

        public IEnumerable<History> HistoryList()
        {
            History history = new History();

            List<History> list = new List<History>();

            SqlCommand cmd = new SqlCommand("SP_ListarHistorial", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                history = new History();

                history.history_ID = Convert.ToInt32(dr["historial_ID"]);
                history.date = Convert.ToDateTime(dr["fecha"]);
                history.description = dr["descripcion"].ToString();
                history.productName = dr["nombre_prod"].ToString();
                history.product_ID = Convert.ToInt32(dr["producto_ID"]);

                list.Add(history);
            }

            dr.Close(); cn.Disconnect();

            return list;
        }
    }
}