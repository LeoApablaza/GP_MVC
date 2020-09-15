using Panaderia_Gestion.Models;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class SaleController : Controller
    {
        SaleViewModel SVM = new SaleViewModel();
        ProductoViewModel PVM = new ProductoViewModel();
        ClientViewModel CVM = new ClientViewModel();

        
        public ActionResult Index()
        {
            IEnumerable<Sales> saleList = SVM.SaleList();

            return View(saleList);
        }

        public ActionResult Create()
        {
            
            ViewBag.ProductList = new SelectList(SVM.ProductsList(), "product_ID", "name");
            ViewBag.WayToPayList = new SelectList(SVM.WayToPayListing(), "wayToPay_ID", "wayToPay");
            ViewBag.ClientList = new SelectList(CVM.ClientsList(), "client_ID", "name");
            ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Sales sales)
        {

            if(ModelState.IsValid)
            {
                SVM.Create(sales);

                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.ProductList = new SelectList(SVM.ProductsList(), "product_ID", "name");
                ViewBag.WayToPayList = new SelectList(SVM.WayToPayListing(), "wayToPay_ID", "wayToPay");
                ViewBag.ClientList = new SelectList(CVM.ClientsList(), "client_ID", "name");
                ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                return View();
            }

        }

        [HttpGet]
        public JsonResult SaleType(int prod_ID)
        {
            Sales saleType = SVM.SaleType(prod_ID);

            return Json(saleType, JsonRequestBehavior.AllowGet);
        }

       


        public JsonResult ComboEdit(int prod_ID, string cantidad_ID)
        {

            try
            {
                double cantidad = Convert.ToDouble(cantidad_ID);

                Sales productoModificado = SVM.ComboEdit(prod_ID, cantidad);

                return Json(productoModificado, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Sales sales = new Sales();


                return Json(sales, JsonRequestBehavior.AllowGet);
            }
        }
        
       
        public ActionResult Edit(int id)
        {

            Sales sales = SVM.getSales(id);

            ViewBag.WayToPayList = new SelectList(SVM.WayToPayListing(), "wayToPay_ID", "wayToPay", sales.wayToPay_ID);
            ViewBag.ClientList = new SelectList(CVM.ClientsList(), "client_ID", "name", sales.client_ID);
            ViewBag.TypeList = new SelectList(SVM.SaleTypeByID(id), "saleType_ID", "saleType");
            ViewBag.ProductList = new SelectList(SVM.ProductsList(), "product_ID", "name", sales.product_ID);

            return View(sales);
        }

        [HttpPost]
        public ActionResult Edit(Sales sales)
        {
            return View();
        }
    }
}