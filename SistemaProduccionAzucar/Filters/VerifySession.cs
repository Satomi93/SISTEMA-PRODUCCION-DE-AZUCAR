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
                if (usuario.tipo_usuario == "Administrador")
                {
                    if (!(filterContext.Controller is UserController))
                    {
                        filterContext.HttpContext.Response.Redirect("~/User/");
                    }
                }
                else if (usuario.tipo_usuario == "Administrador finca")
                {
                    if (!(filterContext.Controller is FincasController || filterContext.Controller is MateriaPrimaController))
                    {
                        filterContext.HttpContext.Response.Redirect("~/Fincas/");
                    }
                }
                else if (usuario.tipo_usuario == "Encargado transporte caña")
                {
                    if (!(filterContext.Controller is VehiculosController
                        || filterContext.Controller is MotoristasController
                        || filterContext.Controller is PedidosTransporteController))
                    {
                        filterContext.HttpContext.Response.Redirect("~/Vehiculos/");
                    }
                }
                else if (usuario.tipo_usuario == "Jefe de producción")
                {
                    if (!(filterContext.Controller is MateriaPrimaCentralController
                        || filterContext.Controller is InventarioProduccionController
                        || filterContext.Controller is PedidosJefeProdController))
                    {
                        filterContext.HttpContext.Response.Redirect("~/InventarioProduccion/");
                    }
                }
                else if (usuario.tipo_usuario == "Presidente de central azucarero")
                {
                    if (!(filterContext.Controller is ReporteriaController))
                    {
                        filterContext.HttpContext.Response.Redirect("~/Reporteria/");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}