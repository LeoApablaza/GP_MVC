using Microsoft.Ajax.Utilities;
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
    public class ProductoViewModel
    {
        Conexion cn = new Conexion();
        
        //Lista de productos para el metodo Index
        public IEnumerable<Products> ProductsList()
        {
            List<Products> products = new List<Products>();

            
            SqlCommand cmd = new SqlCommand("SP_ListarProducto", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Products p = new Products();
                p.product_ID = Convert.ToInt32(dr["producto_ID"]);
                p.name = dr["nombre_prod"].ToString();
                p.price = Convert.ToDecimal(dr["precio"]);
                p.sale_type = dr["tipo_venta"].ToString();
                if(dr["codigo_barra"] != DBNull.Value)
                    p.bar_code = dr["codigo_barra"].ToString();

                products.Add(p);
            }
            dr.Close();
            cn.Disconnect();

            return products;
        }

        public bool Create(Products p)
        {
            SqlCommand cmd = new SqlCommand("SP_CargarProducto", cn.Connect());
            cmd.Parameters.AddWithValue("@nombre", p.name);
            cmd.Parameters.AddWithValue("@precio", p.price);
            cmd.Parameters.AddWithValue("@tipo_venta_ID", p.sale_type_ID);
            cmd.Parameters.AddWithValue("@codigo_barra", p.bar_code);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();
            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return false;
        }

        //Lista para llenar el DropDownListFor
        public IEnumerable<Products> SaleTypeListing()
        {
            List<Products> TypeList = new List<Products>();

            Products products = new Products();

            SqlCommand cmd = new SqlCommand("SP_ListarTipoVenta", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            
            while(dr.Read())
            {
                products = new Products();

                products.sale_type_ID = Convert.ToInt32(dr["tipo_venta_ID"]);
                products.sale_type = dr["tipo_venta"].ToString();

                TypeList.Add(products);
            }

            dr.Close(); cn.Disconnect();

            return TypeList;
        }

        

        public Products GetProducts(int id)
        {
            Products products = new Products();
 
            SqlCommand cmd = new SqlCommand("SP_MostrarProducto", cn.Connect());
            cmd.Parameters.AddWithValue("@producto_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                products.product_ID = Convert.ToInt32(dr["producto_ID"]);
                products.name = dr["nombre_prod"].ToString();
                products.price = Convert.ToDecimal(dr["precio"]);
                products.sale_type_ID = Convert.ToInt32(dr["tipo_venta_ID"]);
                products.sale_type = dr["tipo_venta"].ToString();

                if (dr["codigo_barra"] != DBNull.Value)
                    products.bar_code = dr["codigo_barra"].ToString();

            }
            dr.Close();
            cn.Disconnect();

            return products;
        }

        public bool Edit(Products p)
        {
            SqlCommand cmd = new SqlCommand("SP_EditarProducto", cn.Connect());
            cmd.Parameters.AddWithValue("@producto_ID", p.product_ID);
            cmd.Parameters.AddWithValue("@nombre_prod", p.name);
            cmd.Parameters.AddWithValue("@precio", p.price);
            cmd.Parameters.AddWithValue("@tipo_venta_ID", p.sale_type_ID);
            if(p.bar_code != null)
                cmd.Parameters.AddWithValue("@codigo_barra", p.bar_code);
            else
                cmd.Parameters.AddWithValue("@codigo_barra", "");

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
            SqlCommand cmd = new SqlCommand("SP_EliminarProducto", cn.Connect());

            cmd.Parameters.AddWithValue("@producto_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0)
                return true;
            else
                return false;
        }

        public bool ExistBarCode(Products product)
        {
            string sql;
            int i;

            if (product.product_ID == 0)
                sql = $"SELECT COUNT (codigo_barra) FROM producto WHERE codigo_barra = '{product.bar_code}' AND activo = 1";
            else
                sql = $"SELECT COUNT (codigo_barra) FROM producto WHERE codigo_barra = '{product.bar_code}' AND activo = 1 AND producto_ID <> {product.product_ID}";


            SqlCommand cmd = new SqlCommand(sql, cn.Connect());

            try
            {
                i = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                i = 1;
            }

            cn.Disconnect();

            if (i > 0)
                return true;
            else
            {
                
                return false;
            }
                
        }

       
        public bool ExistDataBase(Products product)
        {

            string sql;
            int i;

            if (product.product_ID == 0)
                sql = $"SELECT COUNT (nombre_prod) FROM producto WHERE nombre_prod = '{product.name}' AND activo = 1";
            else
                sql = $"SELECT COUNT (nombre_prod) FROM producto WHERE nombre_prod = '{product.name}' AND producto_ID <> {product.product_ID} AND activo = 1";

            SqlCommand cmd = new SqlCommand(sql, cn.Connect());

            try
            {
                i = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                i = 1;
            }

            cn.Disconnect();


            if (i > 0)
                return true;
            else
                return false;

        }
    }
}