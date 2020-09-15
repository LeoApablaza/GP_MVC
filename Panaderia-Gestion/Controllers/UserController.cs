using Panaderia_Gestion.Models;
using Panaderia_Gestion.Tools;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class UserController : Controller
    {
        UserViewModel UVM = new UserViewModel();
        User user = new User();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string pass)
        { 
            try
            { 
                bool existe = UVM.VerifyLogin(userName, pass);

                if (existe)
                    return RedirectToAction("Index", "Home");
                else
                { 
                    ModelState.AddModelError("password", "El nombre de usuario o contraseña son incorrectos");
                    return View(user);
                }

            }
            catch
            {
                return View(user);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection UserInfo)
        {
            try
            {

                if (UVM.Create(UserInfo))
                    return RedirectToAction("Index", "Home");
                else
                {
                    ModelState.AddModelError("password", "El nombre de usuario o contraseña son incorrectos");

                    return View(user);
                }

            }
            catch
            {
                return View(user);
            }

        }

        public ActionResult ForgotPass()
        {
            return View();
        }
    }
}