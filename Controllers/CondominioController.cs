using DataModels;
using PVI_ProyectoFinal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_ProyectoFinal.Controllers
{
    public class CondominioController : Controller
    {
        // GET: ConsultarCobros
        public ActionResult ConsultarCobros()
        {
            var list = new List<SpConsultarCobrosResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.SpConsultarCobros(null, null, null, null).ToList();
            }
            return View(list);
        }

        public ActionResult GestionarCobro(int? id)
        {
            var cobro = new ModelCobro
            {
                ServiciosSeleccionados = new List<int>() // Initialize as an empty list
            };

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                if (id != null && id > 0)
                {
                    var cobroResult = db.SpConsultarCobros(null, null, null, true)
                        .Where(c => c.Id_cobro == id)
                        .ToList(); // Ensure the reader is closed

                    cobro = cobroResult.Select(c => new ModelCobro
                    {
                        Id = c.Id_cobro,
                        Monto = c.Monto ?? 0,
                        Mes = c.Mes ?? 1,
                        Anno = c.Anno ?? 2024,
                        Estado = c.Estado,
                        Cliente = c.Cliente,
                        Casa = c.Nombre_casa,
                        ServiciosSeleccionados = db.SpGetServiciosPorCobro(c.Id_cobro)
                            .Select(s => s.Id_servicio)
                            .ToList()
                    }).FirstOrDefault();

                    // If no servicios are returned, initialize the list
                    if (cobro.ServiciosSeleccionados == null)
                    {
                        cobro.ServiciosSeleccionados = new List<int>();
                    }
                }

                // Populate dropdown for Casas
                ViewBag.Casas = db.SpRetornaCasasActivas()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id_casa.ToString(),
                        Text = c.Nombre_casa
                    })
                    .ToList();

                // Populate dropdown for Servicios
                ViewBag.Servicios = db.Servicios.Where(s => s.Estado == true)
                    .Select(s => new SelectListItem
                    {
                        Value = s.IdServicio.ToString(),
                        Text = s.Nombre
                    })
                    .ToList();
            }
            return View(cobro);
        }





        [HttpPost]
        public ActionResult GestionarCobro(ModelCobro cobro)
        {
            string resultado;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    if (cobro.Id == 0)
                    {
                        // Insert new Cobro
                        db.SpCrearCobro(
                            cobro.IdCasa,
                            cobro.Mes,
                            cobro.Anno,
                            cobro.Monto,
                            string.Join(",", cobro.ServiciosSeleccionados ?? new List<int>()),
                            1 // Assuming authenticated user ID
                        );
                    }
                    else
                    {
                        // Update existing Cobro
                        db.SpActualizarCobro(
                            cobro.Id,
                            cobro.Monto,
                            string.Join(",", cobro.ServiciosSeleccionados ?? new List<int>()),
                            1 // Assuming authenticated user ID
                        );
                    }
                    resultado = "Cobro actualizado exitosamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error al guardar el cobro: {ex.Message}";
            }

            ViewBag.Resultado = resultado;
            return RedirectToAction("ConsultarCobros");
        }



        // New AJAX method: GetCasas
        public JsonResult GetCasas()
        {
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                var casas = db.SpRetornaCasasActivas()
                    .Select(c => new { Id = c.Id_casa, Nombre = c.Nombre_casa })
                    .ToList();

                return Json(casas, JsonRequestBehavior.AllowGet);
            }
        }

        // New AJAX method: GetServicios
        public JsonResult GetServicios()
        {
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                var servicios = db.Servicios
                    .Where(s => s.Estado == true)
                    .Select(s => new { Id = s.IdServicio, Nombre = s.Nombre })
                    .ToList();

                return Json(servicios, JsonRequestBehavior.AllowGet);
            }
        }



        // GET: ConsultarCasas
        public ActionResult ConsultarCasas()
        {
            var list = new List<SpListarCasasResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.SpListarCasas(null).ToList();
            }
            return View(list);
        }








        // GET: Crear Casa
        public ActionResult CrearCasa(int? id)
        {
            var casa = new ModelCasa();

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {

                casa = db.SpListarCasas().Select(e => new ModelCasa


                {
                    IdCasa = casa.IdCasa,
                    NombreCasa = casa.NombreCasa,
                    MetrosCuadrados = casa.MetrosCuadrados,
                    NumeroHabitaciones = casa.NumeroHabitaciones,
                    NumeroBanos = casa.NumeroBanos,
                    NombrePersona = casa.NombrePersona,
                    FechaConstruccion = casa.FechaConstruccion,
                    Estado = casa.Estado,

                }).FirstOrDefault();



                // Populate dropdown for Persona
                ViewBag.persona = db.SpRetornaPersona()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id_persona.ToString(),
                        Text = p.Nombre_persona
                    })
                    .ToList();


            }
            return View(casa);
        }






  




       [HttpPost]
        public JsonResult CrearCasa(ModelCasa casa)
        {
            string resultado;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    db.SpCrearCasa(
                        casa.NombreCasa,
                        casa.MetrosCuadrados,
                        casa.NumeroHabitaciones,
                        casa.NumeroBanos,
                        casa.NombrePersona,
                        casa.FechaConstruccion,
                        casa.Estado

                    );
                    resultado = "La casa ha sido registrada exitosamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error al registrar la casa: {ex.Message}";
            }
            return Json(resultado);
        }

    


    //////New method to handle the deletion process.This will use the stored procedure SpEliminarCobro.


    [HttpPost]
        public ActionResult EliminarCobro(int id)
        {
            string resultado;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    db.SpEliminarCobro(id, 1); // Assuming 1 is the authenticated user's ID
                    resultado = "Cobro eliminado exitosamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error al eliminar el cobro: {ex.Message}";
            }

            TempData["Resultado"] = resultado;
            return RedirectToAction("ConsultarCobros");
        }




    }


}
