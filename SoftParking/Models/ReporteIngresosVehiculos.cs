using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class ReporteIngresosVehiculos
    {
        public string Dominio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public string TipoVehiculo { get; set; }
        public bool Estado { get; set; }

        public string getStyleRow()
        {
            return (Estado) ? "ingresado" : "retirado";
        }
    }
}