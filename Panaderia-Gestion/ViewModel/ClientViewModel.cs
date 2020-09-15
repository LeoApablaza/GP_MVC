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
    public class ClientViewModel
    {
        Conexion cn = new Conexion();

        public IEnumerable<Client> ClientsList()
        {
            List<Client> clients = new List<Client>();

            Client client; SqlDataReader dr;

            SqlCommand cmd = new SqlCommand("SP_ListarClientes", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                client = new Client();

                client.client_ID = Convert.ToInt32(dr["cliente_ID"]);
                client.name = dr["nombre_cliente"].ToString();
                client.phone = Convert.ToInt64(dr["telefono"]);
                client.debt = Convert.ToBoolean(dr["tiene_deuda"]);
                client.active = Convert.ToBoolean(dr["activo"]);

                clients.Add(client);

            }

            dr.Close(); cn.Disconnect();

            return clients;
        }

        public bool Create(Client c)
        {
            SqlCommand cmd = new SqlCommand("SP_CargarCliente", cn.Connect());

            cmd.Parameters.AddWithValue("@nombre_cliente", c.name);
            cmd.Parameters.AddWithValue("@telefono", c.phone);
            cmd.Parameters.AddWithValue("@tiene_deuda", c.debt);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return
                    false;
        }

        public Client GetClients(int id)
        {
            SqlCommand cmd = new SqlCommand("SP_MostrarCliente", cn.Connect());

            cmd.Parameters.AddWithValue("@cliente_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr;

            Client c = new Client();

            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                c.client_ID = Convert.ToInt32(dr["cliente_ID"]);
                c.name = dr["nombre_cliente"].ToString();
                c.phone = Convert.ToInt64(dr["telefono"]);
                c.debt = Convert.ToBoolean(dr["tiene_deuda"]);
            }

            dr.Close(); cn.Disconnect();

            return c;

        }

        public bool Edit(Client c)
        {
            SqlCommand cmd = new SqlCommand("SP_EditarCliente", cn.Connect());
            cmd.Parameters.AddWithValue("@cliente_ID", c.client_ID);
            cmd.Parameters.AddWithValue("@nombre_cliente", c.name);
            cmd.Parameters.AddWithValue("@telefono", c.phone);
            cmd.Parameters.AddWithValue("@tiene_deuda", c.debt);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();
            cn.Disconnect();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("SP_EliminarCliente", cn.Connect());
            cmd.Parameters.AddWithValue("@cliente_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0) return true;

            else return false;
        }

        public bool ExistDataBase(Client c)
        {
            string sql;
            int i;
            if (c.client_ID == 0)
                sql = $"SELECT COUNT(nombre_cliente) FROM cliente WHERE nombre_cliente = '{c.name}' AND activo = 1";
            else
                sql = $"SELECT COUNT(nombre_cliente) FROM cliente WHERE nombre_cliente = '{c.name}' AND cliente_ID <> {c.client_ID} AND activo = 1";

            SqlCommand cmd = new SqlCommand(sql, cn.Connect());

            try
            {
                i = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                i = 1;
                string error = ex.Message;
            }
            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return false;
        }
    }
}