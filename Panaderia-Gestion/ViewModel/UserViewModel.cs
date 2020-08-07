using Panaderia_Gestion.Models;
using Panaderia_Gestion.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Panaderia_Gestion.ViewModel
{
    public class UserViewModel
    {
        Conexion cn = new Conexion();
        public bool Create(User u)
        {
            SqlCommand cmd = new SqlCommand("insert into Usuario values (@nombre_usuario, @contraseña, @correo)");
            cmd.Connection = cn.Connect();
            cmd.Parameters.AddWithValue("@nombre_usuario", u.name);
            cmd.Parameters.AddWithValue("@contraseña", u.password);
            cmd.Parameters.AddWithValue("@correo", u.email);

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return false;
        }

        public bool VerifyLogin(User usuario)
        {
            string sql;
            int i;
            sql = $"SELECT COUNT (nombre_usuario) FROM Usuario WHERE nombre_usuario = '{usuario.name}' AND  contraseña = '{usuario.password}'";
            
            SqlCommand cmd = new SqlCommand(sql, cn.Connect());

            try
            {
                i = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                i = 0;
            }

            cn.Disconnect();


            if (i > 0)
                return true;
            else
                return false;

        }

        public bool ExistDataBase(User usuario)
        {
            
            string sql;
            int i;

            if (usuario.user_ID == 0)
                sql = $"SELECT COUNT (nombre_usuario) FROM Usuario WHERE nombre_usuario = '{usuario.name}'";
            else
                sql = $"SELECT COUNT (nombre_usuario) FROM Usuario WHERE nombre_usuario = '{usuario.name}' AND usuario_ID <> {usuario.user_ID}";

            SqlCommand cmd = new SqlCommand(sql, cn.Connect());

            try
            {
                i = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                i = 0;
            }

            cn.Disconnect();
            

            if (i > 0)
                return true;
            else
                return false;

        }
    }
}