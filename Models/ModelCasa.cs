using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelCasa : Casa
    {
        // Maps the Estado field as a boolean for convenience in the views
        public bool _Estado
        {
            get => Estado;
            set => Estado = value;
        }

        // Additional properties specific to the view
        public int IdPersona { get; set; } // The ID of the owner
        public string NombrePersona { get; set; } // Display name of the owner for the dropdown
    }
}