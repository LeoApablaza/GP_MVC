using Panaderia_Gestion.Models;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class ClientController : Controller
    {
        ClientViewModel CVM = new ClientViewModel();
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<Client> List = CVM.ClientsList();

            return View(List);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {

            if (ModelState.IsValid)
            {
                if(CVM.ExistDataBase(client))
                {
                    ModelState.AddModelError("name", "Este cliente ya existe");

                    return View(client);
                }
                else
                { 
                CVM.Create(client);

                return RedirectToAction("Index");
                }

            }
            else
                return View(client);
        }

        public ActionResult Edit(int id)
        {
            Client client = new Client();

            client = CVM.EditGet(id);

            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client c)
        {
            if(ModelState.IsValid)
                if(CVM.ExistDataBase(c))
                {
                    ModelState.AddModelError("name", "Este cliente ya existe");
                    return View(c);
                }
                else
                {
                    CVM.EditPost(c);
                    return RedirectToAction("Index");
                }
            
            return View(c);
        }
    }
}