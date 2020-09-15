using Panaderia_Gestion.Models;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class HistoryController : Controller
    {
        HistoryViewModel HVM = new HistoryViewModel();

        public ActionResult Index()
        {
            IEnumerable<History> histories = HVM.HistoryList();

            return View(histories);
        }
    }
}