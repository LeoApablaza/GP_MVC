using Panaderia_Gestion.Models;
using Panaderia_Gestion.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.ViewModel
{
    public class UserViewModel
    {
        private User user = new User();
        Conexion cn = new Conexion();
        public bool Create(FormCollection u)
        {
            SqlCommand cmd = new SqlCommand("SP_agregarUsuario");
            cmd.Connection = cn.Connect();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombre_usuario", u["userName"]);
            cmd.Parameters.AddWithValue("@nombre", u["name"]);
            cmd.Parameters.AddWithValue("@apellido", u["lastName"]);
            cmd.Parameters.AddWithValue("@contraseña",Encrypt.GetSHA256(u["pass"]));
            cmd.Parameters.AddWithValue("@contraseña2", Encrypt.GetSHA256(u["repeatPass"]));
            cmd.Parameters.AddWithValue("@correo", u["email"]);

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return false;
        }

        public bool VerifyLogin(string userName, string pass)
        {
            int i;
           
            SqlCommand cmd = new SqlCommand("SP_VerificarSesion", cn.Connect());
            cmd.Parameters.AddWithValue("@nombreUsuario", userName);
            cmd.Parameters.AddWithValue("@contraseña", Encrypt.GetSHA256(pass));

            cmd.CommandType = CommandType.StoredProcedure;

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