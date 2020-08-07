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
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.ProductList = new SelectList(PVM.ProductsList(), "product_ID", "name");
            ViewBag.WayToPayList = new SelectList(SVM.WayToPayListing(), "wayToPay_ID", "wayToPay");
            ViewBag.ClientList = new SelectList(CVM.ClientsList(), "client_ID", "name");
            ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sales sales)
        {
            return View();
        }
    }
}