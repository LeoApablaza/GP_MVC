﻿using Panaderia_Gestion.Models;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class HomeController : Controller
    {
        HomeViewModel HVM = new HomeViewModel();

        // GET: Home
        public ActionResult Index()
        {
            Sales sales = HVM.EarningsMonthly();

            sales.price = sales.total * 12;

            return View(sales);
        }
    }
}