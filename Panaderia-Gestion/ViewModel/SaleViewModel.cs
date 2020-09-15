using Antlr.Runtime.Misc;
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


        public IEnumerable<Sales> SaleList()
        {
            Sales s = new Sales();

            List<Sales> sales = new List<Sales>();

            SqlCommand cmd = new SqlCommand("SP_ListarVentas", cn.Connect());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                s = new Sales();

                s.sale_ID = Convert.ToInt32(dr["venta_ID"]);

                if (dr["fecha"] != DBNull.Value)
                    s.date = Convert.ToDateTime(dr["fecha"]).ToShortDateString();

                s.amount = Convert.ToDouble(dr["cantidad"]);
                s.clientName = dr["nombre_cliente"].ToString();
                
                s.clientDebt = Convert.ToBoolean(dr["tiene_deuda"]);
                s.productName = dr["nombre_prod"].ToString();
                s.price = Convert.ToDecimal(dr["precio"]);
                s.wayToPay = dr["forma_pago"].ToString();
                s.total = Convert.ToDecimal(dr["total"]);

                sales.Add(s);
            }

            dr.Close(); cn.Disconnect();

            return sales;
        }

        public List<Products> ProductsList()
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

                products.Add(p);
            }
            dr.Close();
            cn.Disconnect();

            return products;
        }


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

        //Llenar comboBox en cascada
        public Sales SaleType(int id)
        {

            Sales saleType = new Sales();

            SqlCommand cmd = new SqlCommand("SP_TraerTipoVenta", cn.Connect());

            cmd.Parameters.AddWithValue("@producto_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    saleType = new Sales();
                    saleType.saleType_ID = Convert.ToInt32(dr["tipo_venta_ID"]);
                    saleType.saleType = dr["tipo_venta"].ToString();
                }

                dr.Close(); cn.Disconnect();

                return saleType;
        }

        public IEnumerable<Sales> SaleTypeByID(int id)
        {
            List<Sales> saleTypeList = new List<Sales>();

            Sales saleType = new Sales();

            SqlCommand cmd = new SqlCommand("SP_ListarTipoVentaPorVentaID", cn.Connect());

            cmd.Parameters.AddWithValue("@venta_ID", id);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                saleType = new Sales();
                saleType.saleType_ID = Convert.ToInt32(dr["tipo_venta_ID"]);
                saleType.saleType = dr["tipo_venta"].ToString();

                saleTypeList.Add(saleType);
            }

            dr.Close(); cn.Disconnect();

            return saleTypeList;
        }


        public Sales ComboEdit(int prod_ID, double cantidad)
        {
            Sales products = new Sales();

            SqlCommand cmd = new SqlCommand("SP_EdicionVentaOpciones", cn.Connect());
            cmd.Parameters.AddWithValue("@producto_ID", prod_ID);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                products.price = Convert.ToDecimal(dr["precio"]);
                products.saleType = dr["tipo_venta"].ToString();
                products.total = Convert.ToDecimal(dr["ingreso"]);
            }

            dr.Close(); cn.Disconnect();

            return products;
        }


        public bool Create(Sales sales)
        {
            SqlCommand cmd = new SqlCommand("SP_CargarVenta", cn.Connect());
            cmd.Parameters.AddWithValue("@cantidad", sales.amount);
            cmd.Parameters.AddWithValue("@producto_ID", sales.product_ID);
            cmd.Parameters.AddWithValue("@cliente_ID", sales.client_ID);
            cmd.Parameters.AddWithValue("@formaDePago_ID", sales.wayToPay_ID);

            cmd.CommandType = CommandType.StoredProcedure;

            int i = cmd.ExecuteNonQuery();

            cn.Disconnect();

            if (i > 0) return true;
            else return false;
        }

        public Sales getSales(int id)
        {
            Sales sales = new Sales();

            SqlCommand cmd = new SqlCommand("SP_ListarVentaPorId", cn.Connect());
            cmd.Parameters.AddWithValue("@venta_ID", id);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                sales.sale_ID = Convert.ToInt32(dr["venta_ID"]);
                sales.client_ID = Convert.ToInt32(dr["cliente_ID"]);
                sales.product_ID = Convert.ToInt32(dr["producto_ID"]);
                sales.wayToPay_ID = Convert.ToInt32(dr["forma_pago_ID"]);
                sales.saleType_ID = Convert.ToInt32(dr["tipo_venta_ID"]);

                if (dr["fecha"] != DBNull.Value)
                sales.date = Convert.ToDateTime(dr["fecha"]).ToShortDateString();

                sales.amount = Convert.ToDouble(dr["cantidad"]);
                sales.clientName = dr["nombre_cliente"].ToString();
                sales.clientDebt = Convert.ToBoolean(dr["tiene_deuda"]);
                sales.productName = dr["nombre_prod"].ToString();
                sales.price = Convert.ToDecimal(dr["precio"]);
                sales.wayToPay = dr["forma_pago"].ToString();
                sales.total = Convert.ToDecimal(dr["total"]);
            }

            dr.Close(); cn.Disconnect();

            return sales;

        }

       
    }
}