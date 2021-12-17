using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class TipoEstadias
    {
        public long IdTipoEstadia { get; set; }
        public string Tipo { get; set; }

        public string TipoMonto { get; set; }
        public decimal Monto { get; set; }

        public decimal NuevoMonto { get; set; }

        public double Horas { get; set; }
    }
}