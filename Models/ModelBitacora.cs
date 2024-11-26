using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelBitacora
    {
        public int IdBitacora { get; set; }
        public string Detalle { get; set; }
        public int IdUser { get; set; } // FK to Persona
        public DateTime Fecha { get; set; }
        public int IdCobro { get; set; } // FK to Cobro
    }
}