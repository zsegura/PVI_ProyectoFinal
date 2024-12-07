﻿using Antlr.Runtime.Misc;
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
        public ActionResult ConsultarCobros(string clienteNombre, int? mes, int? anno, int? idPersona)
        {
            var list = new List<SpConsultarCobrosResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                // Fetch filtered cobros
                list = db.SpConsultarCobros(clienteNombre, mes, anno, null, idPersona)
                         .OrderByDescending(c => c.Id_cobro) // Order by ID descending
                         .ToList();

                // Populate the dropdown for active persons
                ViewBag.PersonasActivas = db.SpRetornaPersonasActivas()
                                            .Select(p => new SelectListItem
                                            {
                                                Value = p.IdPersona.ToString(), // Pass the ID as the value
                                                Text = p.NombreCompleto
                                            })
                                            .ToList();
            }

            // Pass filters to ViewBag for prepopulation in the view
            ViewBag.ClienteNombre = clienteNombre;
            ViewBag.Mes = mes;
            ViewBag.Anno = anno;
            ViewBag.IdPersona = idPersona;

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
                    var cobroResult = db.SpConsultarCobros(null, null, null, true, null) // Provide null for idPersona
                    .Where(c => c.Id_cobro == id)
                    .ToList();
                    // Ensure the reader is closed

                    cobro = cobroResult.Select(c => new ModelCobro
                    {
                        Id = c.Id_cobro,
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

                // Populate dropdown for Clients
                ViewBag.Clientes = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Seleccione un cliente" }
        }
                .Concat(db.SpRetornaPersonasActivas()
                    .OrderBy(p => p.NombreCompleto)
                    .Select(p => new SelectListItem
                    {
                        Value = p.IdPersona.ToString(),
                        Text = p.NombreCompleto
                    }))
                    .ToList();

                // Populate dropdown for Casas
                ViewBag.Casas = db.SpRetornaCasasActivas()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id_casa.ToString(),
                        Text = c.Nombre_casa
                    })
                    .ToList();

                // Populate dropdown for Years (2024 to 2034)
                ViewBag.AnnoOptions = Enumerable.Range(2024, 2034 - 2024 + 1)
                    .Select(y => new SelectListItem
                    {
                        Value = y.ToString(),
                        Text = y.ToString()
                    })
                    .ToList();

                // Populate dropdown for Months
                ViewBag.MesOptions = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                    .Where(m => !string.IsNullOrEmpty(m)) // Filter out any empty strings
                    .Select((name, index) => new SelectListItem
                    {
                        Value = (index + 1).ToString(), // Month value (1-based index)
                        Text = name // Month name
                    })
                    .ToList();

                // Populate checkboxes for Servicios
                ViewBag.Servicios = db.Servicios
            .Where(s => s.Estado == true) // Only active services
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


        //New action to handle the DetalleCobro view
        public ActionResult DetalleCobro(int id)
        {
            SpConsultarCobrosResult cobroDetails;
            List<ModelBitacora> bitacoraList;
            List<SelectListItem> serviciosAsociados;

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                // Get Cobro details
                cobroDetails = db.SpConsultarCobros(null, null, null, null, null)
                    .FirstOrDefault(c => c.Id_cobro == id);

                // Get associated Bitacora records
                bitacoraList = db.Bitacoras
                    .Where(b => b.IdCobro == id)
                    .OrderByDescending(b => b.IdBitacora)
                    .Select(b => new ModelBitacora
                    {
                        IdBitacora = b.IdBitacora,
                        Detalle = b.Detalle,
                        IdUser = b.IdUser,
                        Fecha = b.Fecha,
                        IdCobro = b.IdCobro
                    })
                    .ToList();

                // Get associated services
                serviciosAsociados = db.SpGetServiciosPorCobro(id)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id_servicio.ToString(),
                        Text = s.Nombre,
                        Selected = true // All associated services are selected
                    })
                    .ToList();
            }

            if (cobroDetails == null)
            {
                TempData["Resultado"] = "No se encontró el cobro especificado.";
                return RedirectToAction("ConsultarCobros");
            }

            ViewBag.BitacoraList = bitacoraList;
            ViewBag.ServiciosAsociados = serviciosAsociados;
            return View(cobroDetails);
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








        GET: Crear Casa
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
