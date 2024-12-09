using Antlr.Runtime.Misc;
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
                list = db.SpConsultarCobros(clienteNombre, mes, anno, idPersona)
                         .OrderByDescending(c => c.Id_cobro)
                         .ToList();

                ViewBag.PersonasActivas = db.SpRetornaPersonasActivas()
                                            .Select(p => new SelectListItem
                                            {
                                                Value = p.IdPersona.ToString(),
                                                Text = p.NombreCompleto
                                            })
                                            .ToList();
            }

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
                ServiciosSeleccionados = new List<int>()
            };

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                if (id.HasValue && id > 0)
                {
                    var cobroResult = db.SpConsultarCobros(null, null, null, null)
                                        .FirstOrDefault(c => c.Id_cobro == id);

                    if (cobroResult != null)
                    {
                        cobro = new ModelCobro
                        {
                            Id = cobroResult.Id_cobro,
                            Mes = cobroResult.Mes ?? 1,
                            Anno = cobroResult.Anno ?? DateTime.Now.Year,
                            Estado = cobroResult.Estado,
                            Cliente = cobroResult.Cliente,
                            IdCliente = cobroResult.Id_cliente,
                            Casa = cobroResult.Nombre_casa,
                            ServiciosSeleccionados = db.SpGetServiciosPorCobro(cobroResult.Id_cobro)
                                                        .Select(s => s.Id_servicio)
                                                        .ToList()
                        };
                    }
                }

                ViewBag.Clientes = db.SpRetornaPersonasActivas()
                                    .OrderBy(p => p.NombreCompleto)
                                    .Select(p => new SelectListItem
                                    {
                                        Value = p.IdPersona.ToString(),
                                        Text = p.NombreCompleto,
                                        Selected = p.IdPersona == cobro.IdCliente
                                    })
                                    .ToList();

                ViewBag.Casas = db.SpRetornaCasasActivas()
                                .Select(c => new SelectListItem
                                {
                                    Value = c.Id_casa.ToString(),
                                    Text = c.Nombre_casa,
                                    Selected = c.Nombre_casa == cobro.Casa
                                })
                                .ToList();

                ViewBag.AnnoOptions = Enumerable.Range(DateTime.Now.Year, 10)
                                                .Select(y => new SelectListItem
                                                {
                                                    Value = y.ToString(),
                                                    Text = y.ToString(),
                                                    Selected = y == cobro.Anno
                                                })
                                                .ToList();

                ViewBag.MesOptions = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                                    .Where(m => !string.IsNullOrEmpty(m))
                                    .Select((name, index) => new SelectListItem
                                    {
                                        Value = (index + 1).ToString(),
                                        Text = name,
                                        Selected = (index + 1) == cobro.Mes
                                    })
                                    .ToList();

                ViewBag.Servicios = db.Servicios
                                     .Where(s => s.Estado == true)
                                     .Select(s => new SelectListItem
                                     {
                                         Value = s.IdServicio.ToString(),
                                         Text = s.Nombre
                                     })
                                     .ToList();
            }

            ViewBag.IsEdit = id.HasValue && id > 0;
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
                    // Check for valid Cliente
                    if (cobro.IdCliente.HasValue && cobro.IdCliente > 0)
                    {
                        var isActive = db.Personas.Any(p => p.IdPersona == cobro.IdCliente && p.Estado == true);
                        if (!isActive)
                        {
                            resultado = "The selected Persona is inactive or does not exist.";
                            TempData["Resultado"] = resultado;
                            return RedirectToAction("ConsultarCobros");
                        }
                    }

                    // Prepare selected services as a comma-separated string
                    string serviciosString = cobro.ServiciosSeleccionados != null
                        ? string.Join(",", cobro.ServiciosSeleccionados.Distinct())
                        : string.Empty;

                    if (cobro.Id == 0) // New Cobro
                    {
                        db.SpCrearCobro(
                            cobro.IdCasa,            // IdCasa
                            cobro.Mes,               // Mes
                            cobro.Anno,              // Anno
                            cobro.Monto,             // Monto
                            serviciosString,         // Selected Services
                            cobro.IdCliente ?? 0     // IdPersona
                        );

                        resultado = "Cobro created successfully.";
                    }
                    else // Existing Cobro
                    {
                        db.SpActualizarCobro(
                            cobro.Id,               // Cobro ID
                            serviciosString         // Updated Services
                        );

                        resultado = "Cobro updated successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error managing Cobro: {ex.Message}";
            }

            TempData["Resultado"] = resultado;
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
                cobroDetails = db.SpConsultarCobros(null, null, null, null)
                                .FirstOrDefault(c => c.Id_cobro == id);

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

                serviciosAsociados = db.SpGetServiciosPorCobro(id)
                                      .Select(s => new SelectListItem
                                      {
                                          Value = s.Id_servicio.ToString(),
                                          Text = s.Nombre,
                                          Selected = true
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








        //GET: Crear Casa
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



                 Populate dropdown for Persona
                ViewBag.persona = db.SpRetornaPersona()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id_persona.ToString(),
                        Text = p.Nombre
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




        //New method to handle the deletion process.This will use the stored procedure SpEliminarCobro.


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


        //Implementation of the Pagar method
        [HttpPost]
        public ActionResult Pagar(int id)
        {
            string resultado;

            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    // Temporarily hardcode userId to 1
                    int userId = 1; 

                    // Call the stored procedure
                    db.SpPagarCobro(
                        id,                 // Cobro ID
                        DateTime.Now,       // Current date as payment date
                        userId              // Authenticated user ID
                    );

                    resultado = "Cobro marked as paid successfully.";
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error paying Cobro: {ex.Message}";
            }

            TempData["Resultado"] = resultado;
            return RedirectToAction("DetalleCobro", new { id });
        }







        // GET: ConsultarServicios 
        public ActionResult ConsuktarServicios ()
        {
            var list = new List<SpListarServicios>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.spListarServicio(null).ToList();
            }
            return View(list);
        }




    }


}
