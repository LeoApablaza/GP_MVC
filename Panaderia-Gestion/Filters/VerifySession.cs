using Panaderia_Gestion.Controllers;
using Panaderia_Gestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        private User user;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);

                user = (User)HttpContext.Current.Session["User"];

                if(user == null)
                {
                    if (filterContext.Controller is UserController == false)
                        filterContext.HttpContext.Response.Redirect("/User/Login");

                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/User/Login");
            }
        }
    }
}