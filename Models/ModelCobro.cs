using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelCobro
    {
        public int IdCobro { get; set; }
        public int? IdCasa { get; set; } // FK to Casa
        public int? Mes { get; set; }
        public int? Anno { get; set; }
        public int? IdBitacora { get; set; } // FK to Bitacora
        public string Estado { get; set; } // "Pendiente", "Pagado", etc.
        public decimal? Monto { get; set; }
        public DateTime? FechaPagada { get; set; }
    }
}