using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models;
using SistemaProduccionAzucar.Models.InventarioProduccion;
using SistemaProduccionAzucar.Models.TableViewModels;
using SistemaProduccionAzucar.Models.Login;

namespace SistemaProduccionAzucar.Controllers
{
    public class InventarioProduccionController : Controller
    {
        // GET: InventarioProduccion
        public ActionResult Index()
        {
            List<InventarioProduccion> list = null;

            using (InventarioProduccionEntities db = new InventarioProduccionEntities())
            {

                list = (from d in db.inventario_produccion
                        orderby d.cod_producto ascending
                        select new InventarioProduccion
                        {
                            cod_producto = d.cod_producto,
                            descripcion = d.descripcion,
                            cantidad = d.cantidad,
                            unidad = d.unidad,
                            tipo_producto = d.tipo_producto,
                        }).ToList();

                return View(list);
            }
        }

        public ActionResult AgregarProducto(string descripcion, string unidad, string tipoProducto)
        {
            using (InventarioProduccionEntities db = new InventarioProduccionEntities() )
            {
                try {

                    var validarTipoProducto = from p in db.inventario_produccion
                                              where p.tipo_producto == tipoProducto
                                              select p;
                    var producto = validarTipoProducto.FirstOrDefault();

                    if (producto.tipo_producto == "melaza")
                    {
                        return Json(new { 
                            response = 0,
                            message = "Melaza ya existe en inventario."
                        });
                    }

                    inventario_produccion pro = new inventario_produccion();
                    pro.descripcion = descripcion;
                    pro.unidad = unidad;
                    pro.tipo_producto = tipoProducto;
                    pro.cantidad = 0;

                    db.inventario_produccion.Add(pro);
                    db.SaveChanges();
                    return Json(new {
                        response = 1,
                        message = "Exito"
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "No se pudo agregar. " + ex.Message
                    });
                }
            }
        }

        public static int productoSeleccionado = 0;
        public ActionResult MostrarRegistros(int cod_producto)
        {
            productoSeleccionado = cod_producto;
            try
            {
                using (InventarioProduccionEntities db = new InventarioProduccionEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;



                    List<RegistroProduccion> list = (from m in db.registro_produccion
                                                     where m.cod_producto == cod_producto
                                                     orderby m.fecha_registro ascending
                                                     select new RegistroProduccion
                                                     {
                                                         correlativo = m.correlativo,
                                                         materia_prima = m.materia_prima,
                                                         cantidad = m.cantidad,
                                                         unidad_m = m.unidad_m,
                                                         estado = m.estado,
                                                         producto = m.producto,
                                                         cantidad_producida = m.cantidad_producida,
                                                         unidad_p = m.unidad_p,
                                                         fecha_registro = m.fecha_registro,
                                                         fecha_fin = m.fecha_fin
                                                     }).ToList();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito",
                        data = list
                    });

                }
            } catch (Exception ex)
            {
                return Json(new{
                    response = 0,
                    message = "Error. " + ex.Message
                });
            }

        }

        public ActionResult AgregarProduccionAzucar(decimal cantidadUtilizar)
        {
            try
            {
                using (InventarioProduccionEntities db = new InventarioProduccionEntities())
                {
                    var usuario = (usuarios)Session["UserData"];

                    registro_produccion pro = new registro_produccion();
                    inventario_central inv = new inventario_central();

                    var findMateria = from i in db.inventario_central
                                  where i.tipo_materia == "melaza"
                                  select i;
                    var materia = findMateria.FirstOrDefault();


                    var findProducto = from p in db.inventario_produccion
                                       where p.cod_producto == productoSeleccionado
                                       select p;

                    var producto = findProducto.FirstOrDefault();


                    if ( materia.cantidad >= cantidadUtilizar)
                    {
                        pro.cod_materia = materia.cod_materia_prima;
                        pro.materia_prima = materia.descripcion;
                        pro.cantidad = cantidadUtilizar;
                        pro.unidad_m = materia.unidad;
                        pro.estado = "En producción";
                        pro.cod_producto = producto.cod_producto;
                        pro.producto = producto.descripcion;
                        pro.cantidad_producida = 0;
                        pro.unidad_p = producto.unidad;
                        pro.fecha_registro = DateTime.Now;

                        db.registro_produccion.Add(pro);

                        decimal? resta = materia.cantidad - cantidadUtilizar;
                        materia.cantidad = resta;
                        db.Entry(materia).State = System.Data.Entity.EntityState.Modified;
                        

                        historial_inventario_central his = new historial_inventario_central();
                        his.cod_materia_prima = materia.cod_materia_prima;
                        his.cantidad = cantidadUtilizar;
                        his.cod_cat = 1;
                        his.comentario = "Por salida para produccion.";
                        his.fecha_creacion = DateTime.Now;
                        his.usuario_creacion = usuario.username;
                        db.historial_inventario_central.Add(his);
                        db.SaveChanges();

                        return Json(new
                        {
                            response = 1,
                            message = "Exito."
                        });

                    }
                    else
                    {
                        return Json(new
                        {
                            response = 0,
                            message = "No hay suficiente melaza en inventario."
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { 
                    response = 0,
                    message = "Error. " + ex.Message
                });
            }
        }

        public ActionResult AgregarProduccionMelaza(decimal cantidadUtilizar)
        {
            try
            {
                using (InventarioProduccionEntities db = new InventarioProduccionEntities())
                {
                    var usuario = (usuarios)Session["UserData"];

                    registro_produccion pro = new registro_produccion();

                    var findMateria = from i in db.inventario_central
                                      where i.tipo_materia == "caña arrimada"
                                      select i;
                    var materia = findMateria.FirstOrDefault();


                    var findProducto = from p in db.inventario_produccion
                                       where p.cod_producto == productoSeleccionado
                                       select p;

                    var producto = findProducto.FirstOrDefault();


                    if (materia.cantidad >= cantidadUtilizar)
                    {
                        pro.cod_materia = materia.cod_materia_prima;
                        pro.materia_prima = materia.descripcion;
                        pro.cantidad = cantidadUtilizar;
                        pro.unidad_m = materia.unidad;
                        pro.estado = "En producción";
                        pro.cod_producto = producto.cod_producto;
                        pro.producto = producto.descripcion;
                        pro.cantidad_producida = 0;
                        pro.unidad_p = producto.unidad;
                        pro.fecha_registro = DateTime.Now;

                        db.registro_produccion.Add(pro);

                        decimal? resta = materia.cantidad - cantidadUtilizar;
                        materia.cantidad = resta;
                        db.Entry(materia).State = System.Data.Entity.EntityState.Modified;

                        historial_inventario_central his = new historial_inventario_central();
                        his.cod_materia_prima = materia.cod_materia_prima;
                        his.cantidad = cantidadUtilizar;
                        his.cod_cat = 1;
                        his.comentario = "Por salida para produccion.";
                        his.fecha_creacion = DateTime.Now;
                        his.usuario_creacion = usuario.username;
                        db.historial_inventario_central.Add(his);
                        db.SaveChanges();

                        return Json(new
                        {
                            response = 1,
                            message = "Exito."
                        });

                    }
                    else
                    {
                        return Json(new
                        {
                            response = 0,
                            message = "No hay suficiente caña arrimada en inventario."
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    response = 0,
                    message = "Error. " + ex.Message
                });
            }
        }

        public ActionResult FinalizarProduccion(int codRegistro, decimal cantidadProducida)
        {
            using (InventarioProduccionEntities db = new InventarioProduccionEntities())
            {
                try { 
                var findRegistro = from r in db.registro_produccion
                          where r.correlativo == codRegistro
                          select r;
                var registro = findRegistro.FirstOrDefault();

                registro.cantidad_producida = cantidadProducida;
                registro.fecha_fin = DateTime.Now;
                registro.estado = "Finalizado";

                var findProducto = from p in db.inventario_produccion
                                   where p.cod_producto == registro.cod_producto
                                   select p;
                var producto = findProducto.FirstOrDefault();

                decimal? suma = producto.cantidad + cantidadProducida;
                producto.cantidad = suma;

                db.Entry(registro).State = System.Data.Entity.EntityState.Modified;
                db.Entry(producto).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();


                    return Json(new { 
                        response = 1,
                        message = "Exito. "
                    });


                }
                catch (Exception ex)
                {
                    return Json(new {
                        response = 0,
                        message = "Error. " + ex.Message
                    });
                }
            }
        }

        public ActionResult ConvertirMateria(decimal cantidadConvertir)
        {
            using (InventarioProduccionEntities db = new InventarioProduccionEntities())
            {
                try
                {
                    var usuario = (usuarios)Session["UserData"];

                    var findMelazaMateria = from m in db.inventario_central
                                            where m.tipo_materia == "melaza"
                                            select m;
                    var melazaMateria = findMelazaMateria.FirstOrDefault();

                    var findMelazaProduccion = from p in db.inventario_produccion
                                               where p.tipo_producto == "melaza"
                                               select p;
                    var melazaProduccion = findMelazaProduccion.FirstOrDefault();

                    if (cantidadConvertir > melazaProduccion.cantidad)
                    {
                        return Json(new { 
                            response = 0,
                            message = "No hay suficiente melaza en melaza producción."
                        });
                    } else
                    {
                        decimal? sumaMateria = melazaMateria.cantidad + cantidadConvertir;
                        melazaMateria.cantidad = sumaMateria;

                        decimal? restaProduccion = melazaProduccion.cantidad - cantidadConvertir;
                        melazaProduccion.cantidad = restaProduccion;

                        db.Entry(melazaMateria).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(melazaProduccion).State = System.Data.Entity.EntityState.Modified;

                        historial_inventario_central his = new historial_inventario_central();
                        his.cod_materia_prima = melazaMateria.cod_materia_prima;
                        his.cantidad = cantidadConvertir;
                        his.cod_cat = 2;
                        his.comentario = "Por ingreso de materia prima.";
                        his.fecha_creacion = DateTime.Now;
                        his.usuario_creacion = usuario.username;
                        db.historial_inventario_central.Add(his);
                        db.SaveChanges();


                        return Json(new { 
                            response = 1,
                            message = "Exito. "
                        });

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { 
                    response = 0,
                    message = "Error. " + ex.Message
                    });
                }
            }
        }
    }
}