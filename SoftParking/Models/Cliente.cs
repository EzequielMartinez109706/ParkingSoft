using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Dominio { get; set; }
        public long IdTipoVehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public long Codigo { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaNulleable { get; set; }
    }
}