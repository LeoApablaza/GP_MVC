using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using Microsoft.Ajax.Utilities;

namespace Panaderia_Gestion.Tools
{
    public class Conexion
    {
        SqlConnection cn;

        public SqlConnection Connect()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

            if (cn.State == ConnectionState.Closed)
                cn.Open();

            return cn;
        }

        public void Disconnect()
        {
            if(cn != null)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                cn = null;
            }

        }
    }
}