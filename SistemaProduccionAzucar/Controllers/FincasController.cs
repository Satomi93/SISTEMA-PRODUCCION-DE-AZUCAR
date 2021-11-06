using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models;
using SistemaProduccionAzucar.Models.Fincas;
using SistemaProduccionAzucar.Models.TableViewModels;
using SistemaProduccionAzucar.Models.Login;
using SistemaProduccionAzucar.Models.MateriaPrima;

namespace SistemaProduccionAzucar.Controllers
{
    public class FincasController : Controller
    {
        // GET: Fincas
        
        readonly FincasEntities datos = new FincasEntities();
        public ActionResult Index()
        {
            var tables = new Models.TableViewModels.FincasViewModel
            {
                tablaFincas = datos.fincas.ToList(),
                tabla_registro_sembrado = datos.registro_sembrado.ToList(),
                tabla_registro_corte = datos.registro_corte.ToList(),
            };
            return View(tables);
        }

        public static int fincaSeleccionada = 0;
        public ActionResult Sembrado(int codFinca)
        {
            fincaSeleccionada = codFinca;

            using (FincasEntities db = new FincasEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                

                List<RegistroSembrado> list = (from m in db.registro_sembrado
                                                       where m.cod_finca == codFinca
                                                       orderby m.cod_sembrado ascending
                                                       select new RegistroSembrado
                                                       {
                                                           cod_sembrado = m.cod_sembrado,
                                                           cantidad_sembrado = m.cantidad_sembrado,
                                                           unidad_sembrado = m.unidad_sembrado,
                                                           cantidad_fertilizante = m.cantidad_fertilizante,
                                                           unidad_fertilizante = m.unidad_fertilizante,
                                                           fecha_registro = m.fecha_registro
                                                       }).ToList();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = list
                });
            }
        }

        public ActionResult Cortes(int codFinca)
        {
            fincaSeleccionada = codFinca;

            using (FincasEntities db = new FincasEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                List<RegistroCorte> list = (from m in db.registro_corte
                                               where m.cod_finca == codFinca
                                               orderby m.cod_corte ascending
                                               select new RegistroCorte
                                               {
                                                   cod_corte = m.cod_corte,
                                                   cantidad_cortada = m.cantidad_cortada,
                                                   unidad = m.unidad,
                                                   fecha_registro = m.fecha_registro
                                               }).ToList();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = list
                });
            }
        }

        public ActionResult AgregarFinca(string nombre, string direccion, decimal area)
        {
            var usuario = (usuarios)Session["UserData"];

            using (var db = new FincasEntities())
            {
                var list = from d in db.fincas
                           where d.nombre == nombre 
                           select d;

                if (list.Count() > 0)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "Nombre de finca ya existe."
                    });
                }
                else
                {
                    SistemaProduccionAzucar.Models.Fincas.fincas finca = new SistemaProduccionAzucar.Models.Fincas.fincas();
                    finca.nombre = nombre;
                    finca.direccion = direccion;
                    finca.fecha_creacion = DateTime.Now;
                    finca.area_metros_cuadrados = area;
                    finca.usuario_creacion = usuario.cod_usuario;

                    db.fincas.Add(finca);
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });
                }
            }
        }

        public ActionResult AgregarSembrado(decimal cantidadSembrada, decimal cantidadFertilizante)
        {
            var usuario = (usuarios)Session["UserData"];
            using (var db = new MateriaPrimaEntities())
            {

                //var listAbono = from d in db.inventario_finca
                //           where d.nombre == "abono" && d.cod_finca == fincaSeleccionada
                //           select d.cantidad;

                var listFertilizante = from d in db.inventario_finca
                                where d.tipo_materia == "fertilizante" && d.cod_finca == fincaSeleccionada
                                select d.cantidad;

                var listBrote = from d in db.inventario_finca
                                       where d.tipo_materia == "semilla" && d.cod_finca == fincaSeleccionada
                                       select d.cantidad;

                var codBrote = from d in db.inventario_finca
                               where d.tipo_materia == "semilla" && d.cod_finca == fincaSeleccionada
                           select d.cod_materia_prima;

                //var codAbono = from d in db.inventario_finca
                //                where d.nombre == "abono" && d.cod_finca == fincaSeleccionada
                //           select d.cod_materia_prima;

                var codFertilizante = from d in db.inventario_finca
                                       where d.tipo_materia == "fertilizante" && d.cod_finca == fincaSeleccionada
                                select d.cod_materia_prima;




                 if (cantidadSembrada > listBrote.FirstOrDefault() || listBrote.FirstOrDefault()  == null)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "No hay suficientes brotes de caña en inventario."
                    });
                }
                else if (cantidadFertilizante > listFertilizante.FirstOrDefault() || listFertilizante.FirstOrDefault() == null)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "No hay suficiente fertilizante en inventario."
                    });
                }
                else
                {
                    using (var ba = new FincasEntities())
                    {
                        var listRegistro = from d in ba.registro_sembrado
                                           select d;

                        SistemaProduccionAzucar.Models.Fincas.registro_sembrado sembrado = new SistemaProduccionAzucar.Models.Fincas.registro_sembrado();
                        sembrado.cantidad_sembrado = cantidadSembrada;
                        sembrado.unidad_sembrado = "kg";
                        sembrado.cod_brote = codBrote.FirstOrDefault();
                        sembrado.cantidad_fertilizante = cantidadFertilizante;
                        sembrado.unidad_fertilizante = "kg";
                        sembrado.cod_fertilizante = codFertilizante.FirstOrDefault();
                        sembrado.cod_finca = fincaSeleccionada;
                        sembrado.fecha_registro = DateTime.Now;
                        sembrado.usuario_creacion = usuario.username;

                        ba.registro_sembrado.Add(sembrado);
                        ba.SaveChanges();


                        return Json(new
                        {
                            response = 1,
                            message = "Éxito"
                        });
                    }
                }
            }
        }

        public ActionResult AgregarCorte(decimal cantidadCortada)
        {

            using (var ba = new FincasEntities())
            {
                var listRegistro = from d in ba.registro_corte
                                   select d;

                SistemaProduccionAzucar.Models.Fincas.registro_corte corte = new SistemaProduccionAzucar.Models.Fincas.registro_corte();
                corte.cantidad_cortada = cantidadCortada;
                corte.unidad = "kg";
                corte.cod_finca = fincaSeleccionada;
                corte.fecha_registro = DateTime.Now;

                ba.registro_corte.Add(corte);
                ba.SaveChanges();

                return Json(new
                {
                    response = 1,
                    message = "Éxito"
                });
            }
        }
    }
}