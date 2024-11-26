using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelPersona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; } // Active or Inactive
        public string TipoPersona { get; set; } // "Empleado" or "Cliente"
    }
}