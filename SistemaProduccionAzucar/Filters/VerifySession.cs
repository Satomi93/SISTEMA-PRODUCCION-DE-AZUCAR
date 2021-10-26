using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Login;
using SistemaProduccionAzucar.Controllers;

namespace SistemaProduccionAzucar.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            filterContext.HttpContext.Response.Cache.SetNoStore();
            var usuario = (usuarios)HttpContext.Current.Session["UserData"];

            if (usuario == null)
            {
                if (!(filterContext.Controller is HomeController))
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            else
            {
                if (filterContext.Controller is HomeController)
                {
                    if (usuario.tipo_usuario == "Administrador")
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/Index");
                    }
                    else if (usuario.tipo_usuario == "Administrador finca")
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/Index");
                    }
                    else if (usuario.tipo_usuario == "Encargado transporte caña")
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/Index");
                    }
                    else if (usuario.tipo_usuario == "Jefe de área")
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/Index");
                    }
                    else if (usuario.tipo_usuario == "Encargado de central")
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/Index");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}