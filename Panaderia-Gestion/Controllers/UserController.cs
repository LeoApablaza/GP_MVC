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
        User usuario = new User();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string nombre, string contraseña)
        {
            usuario.name = nombre;
            usuario.password = contraseña;
            
            bool existe = UVM.VerifyLogin(usuario);
            if (existe)
                return RedirectToAction("Index", "Home");
            else
                return View();
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
                usuario.name = UserInfo["name"];
                usuario.lastName = UserInfo["lastName"];
                usuario.email = UserInfo["email"];

                if (UserInfo["pass"] == UserInfo["repeatPass"])
                    usuario.password = UserInfo["pass"];
                else
                    return View();

                if (UVM.Create(usuario))
                    return RedirectToAction("Index", "Home");
                else
                    return View();

               
            }
            catch(Exception ex)
            {
                return View();
            }

        }

        public ActionResult ForgotPass()
        {
            return View();
        }
    }
}